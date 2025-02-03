using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public int noOfBoidsPerGroup;
    public Vector3 spawnPosition;
    public float spawnRadius;
    public List<Node> nodes = new List<Node>();
    public List<GameObject> boids = new List<GameObject>();

    Grid grid;
    Transform player;
    Vector3 lastPlayerposition;
    GameObject lastBoid;

    public Vector3 averagePos;
    public float steerTowardsAveragePositionWeight;
    public float y = 7f;

    void Start()
    {
        //grid = FindFirstObjectByType<Grid>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        ////Debug.Log($"BoidsManager - {}")
        //nodes = grid.AStar(current.position, player.position);
        //if (nodes == null)
        //{
        //    Debug.Log($"BoidsManager - nodes is null {nodes}");
        //}
        //else if (nodes.Count == 0)
        //{
        //    Debug.Log($"BoidsManager - count == 0 {nodes.Count}");
        //}
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerposition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.DrawRay(GetAveragePosition(), Vector3.up, Color.red);
       //averagePos = GetAveragePosition() * steerTowardsAveragePositionWeight;
    }

    Vector3 GetAveragePosition()
    {
        //if (boids == null)
        float x = 0f;
        float y = 0f;
        float z = 0f;
        foreach (GameObject go in boids)
        {
            Vector3 position = go.transform.position;
            x += position.x;
            y += position.y;
            z += position.z;
        }

        //return new Vector3(x/boids.Count, y/boids.Count, z/boids.Count);
        return new Vector3(x / boids.Count, 0, z / boids.Count);
    }

    void GetLastBoid()
    {
        // This sets lastBoid to the boid furthest away from the player
        // The AStar will be called from this boid. Logic being all other boids will be in front and so can use this path. If other way around, last boid could get stuck trying to cut across rocks


        //        GameObject boid = boids[0];
        //        foreach (GameObject go in boids)
        //        {
        //            if (Vector3.Distance(go.transform.position, player.position) > Vector3.Distance(boid.transform.position, player.position))
        //            {
        //                boid = go;
        //            }
        //        }
        //        lastBoid = boid;
    }
}
