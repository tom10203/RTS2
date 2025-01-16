using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class PathFindingResult
{
    public bool Success { get; }
    public List<Vector3> Path { get; }
    public string ErrorMessage { get; }

    private PathFindingResult(bool success, List<Vector3> path = null, string errorMessage = null)
    {
        Success = success;
        Path = path;
        ErrorMessage = errorMessage;
    }

    public static PathFindingResult Succeeded(List<Vector3> path) => new PathFindingResult(true, path);
    public static PathFindingResult Failed(string message) => new PathFindingResult(false, new List<Vector3>(), errorMessage: message);
}

public class PathFindingOptions
{
    public float DistanceThreshold { get; set; } = 3.0f;
    public int MaxIterations { get; set; } = 500;
    public float DiagonalMovementCost { get; set; } = 1.5f;
    public float StraightMovementCost { get; set; } = 1.5f;
}

public static class EnemyPathFinding
{
    private static readonly Vector3[] Directions = {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right,
        new(1, 0, 1),   // forward + right
        new(-1, 0, 1),  // forward + left
        new(1, 0, -1),  // back + right
        new(-1, 0, -1)  // back + left
    };

    private static readonly bool[] IsDiagonal = {
        false, false, false, false, true, true, true, true
    };

    public static PathFindingResult FindPath(
        Vector3 start,
        Vector3 goal,
        PathFindingOptions options = null)
    {
        options ??= new PathFindingOptions();

        try
        {
            return TryFindPath(start, goal, options);
        }
        catch (System.Exception ex)
        {
            return PathFindingResult.Failed($"Pathfinding failed: {ex.Message}");
        }
    }

    private static PathFindingResult TryFindPath(
        Vector3 start,
        Vector3 goal,
        PathFindingOptions options)
    {
        //Debug.Log($"TRFindPath called, options {options.MaxIterations}");
        MinHeap openSet = new MinHeap();
        HashSet<Vector3> closedSet = new HashSet<Vector3>();
        Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();

        // Initialize starting point
        //gScores[start] = 0;
        closedSet.Add(start);
        openSet.Add(CalculateHeuristic(start, goal, 0), 0, start);
        
        int iterations = 0;
        while (!openSet.IsEmpty && iterations < options.MaxIterations)
        {
            iterations++;
            //Debug.Log($"{iterations} outerloop iterations, openSet.Count {openSet.Count}");
            HeapNode current = openSet.RemoveMin();
            Vector3 currentPos = current.Position;
            float gScore = current.GScore;
            //Debug.Log($"currentPos {currentPos}, gScore {gScore}");

            if ((goal-currentPos).magnitude < options.DistanceThreshold)
            {
                //Debug.Log($"{iterations} Path found");
                return PathFindingResult.Succeeded(ReconstructPath(cameFrom, currentPos));
            }

            for (int i = 0; i < Directions.Length; i++)
            {
                //Debug.Log($"i {i}, direction.Length {Directions.Length}");
                Vector3 neighbor = currentPos + Directions[i] * 2f;

                if (closedSet.Contains(neighbor))
                {
                    //Debug.Log($"neighbor in closed set {neighbor}");
                    continue;
                }


                if (!IsValidPosition(neighbor))
                {
                    //Debug.Log($"Invalid position {neighbor}");
                    continue;
                }

                float movementCost = IsDiagonal[i] ?
                    options.DiagonalMovementCost :
                    options.StraightMovementCost;
                float tentativeGScore = gScore + movementCost;
                float heuristic = CalculateHeuristic(neighbor, goal, tentativeGScore);

                openSet.Add(heuristic, tentativeGScore, neighbor);

                cameFrom[neighbor] = currentPos;

                closedSet.Add(neighbor);
            }
            //Debug.Log($"End of for loop");
        }

        return PathFindingResult.Failed("Path not found within iteration limit");
    }

    private static bool IsValidPosition(Vector3 position)
    {
        Collider[] rockColliders = Physics.OverlapSphere(position, 2f, 1 << 7);
        if (rockColliders.Length > 0)
        {
            //Debug.Log($"rock collider hit, invalid position {rockColliders[0].name}");
            return false;
        }
        else
        {
            return true;
        }
    }

    private static float CalculateHeuristic(Vector3 from, Vector3 to, float gScore)
    {
        return (to-from).magnitude + gScore;
    }

    private static List<Vector3> ReconstructPath(
        Dictionary<Vector3, Vector3> cameFrom,
        Vector3 current)
    {
        var path = new List<Vector3> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        //path.Reverse();
        return path;
    }
}