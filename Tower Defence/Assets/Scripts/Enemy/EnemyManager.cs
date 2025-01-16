using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float stepChange;
    public float spawnPointDistance;
    public float playerDistanceThreshold;
    public float changeDirectionVectorDst;
    public float turningSpeed;
    public GameObject enemy;
    public Transform player;

    Vector3 lastPlayerPosition;
    float playerMoveDst = 0f;

    Vector3 enemySpawnPoint;
    Vector3 directionVector;

    PathFindingResult enemyDirections;



    void Start()
    {
        
        enemySpawnPoint = new Vector3(16f,1f, -32.9f);
        enemy.transform.position = enemySpawnPoint;

        lastPlayerPosition = player.position;

        enemyDirections = EnemyPathFinding.FindPath(enemy.transform.position, player.position);
        print($"PathFindingResult successful {enemyDirections.Success}");

    }

    // Update is called once per frame
    void Update()
    {
        if (HasPlayerMoved())
        {
            lastPlayerPosition = player.position;
            enemyDirections = EnemyPathFinding.FindPath(enemy.transform.position, player.position);

        }

        

        foreach (Vector3 dir in enemyDirections.Path)
        {
            Debug.DrawRay(dir, Vector3.up, Color.red);
        }
       
        directionVector = SetDirectionVector(enemyDirections);
        MoveTowards(directionVector);


    }

    bool HasPlayerMoved()
    {
        float distance = (player.position - lastPlayerPosition).magnitude;
        if (distance > playerDistanceThreshold)
        {
            return true;
        }
        return false;
    }

    void MoveTowards(Vector3 direction)
    {
        Vector3 desiredDirection = direction - enemy.transform.position;
        desiredDirection = new Vector3(desiredDirection.x, 0, desiredDirection.z);
        enemy.transform.position += desiredDirection.normalized * stepChange * Time.deltaTime;

        float singleStep = turningSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(enemy.transform.forward, desiredDirection, singleStep, 0.0f);
        Debug.DrawRay(enemy.transform.position, newDirection * 3, Color.yellow);
        enemy.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    Vector3 SetDirectionVector(PathFindingResult enemyDirections)
    {
        float playerToEnemyDistance = (player.position - enemy.transform.position).magnitude;

        // Remove unnecessary path points that are closer to the player than the enemy
        while (enemyDirections.Path.Count > 0 &&
              playerToEnemyDistance <= (player.position - enemyDirections.Path[enemyDirections.Path.Count - 1]).magnitude)
        {
            enemyDirections.Path.RemoveAt(enemyDirections.Path.Count - 1);
        }

        return enemyDirections.Path.Count > 0 ? enemyDirections.Path[enemyDirections.Path.Count - 1] : Vector3.zero;
    }


}

