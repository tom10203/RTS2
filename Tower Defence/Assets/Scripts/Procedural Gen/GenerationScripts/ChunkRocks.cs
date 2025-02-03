using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkRocks : MonoBehaviour
{
    public float chunkWidth;
    public float rockWidth = 2f;
    public Vector3 centre;

    public float noiseThreshold = 0.5f;
    public float noiseScale = 1f;
    public GameObject rock;

    //void Update()
    //{
    //    Vector3 startPosition = centre + Vector3.left * chunkWidth + Vector3.back * chunkWidth + Vector3.right * rockWidth / 2 + Vector3.forward * rockWidth / 2;
    //    int noOfRocks = (int)chunkWidth / (int)rockWidth;

    //    for (int y = 0; y < noOfRocks; y++)
    //    {
    //        for (int x = 0; x < noOfRocks; x++)
    //        {
    //            Vector3 rockPosition = startPosition + Vector3.forward * rockWidth * y + Vector3.right * rockWidth * x;
    //            Vector2 perlinInput = new Vector2(rockPosition.x, rockPosition.z) / noiseScale;

    //            float perlin = Mathf.PerlinNoise(perlinInput.x, perlinInput.y);

    //            if (perlin > noiseThreshold)
    //            {
    //                //Add draw function here
    //            }

    //        }
    //    }
    //}
    public void SetRockVariables(float chunkWidth, float rockWidth, Vector3 centre, float noiseThreshold, float noiseScale, GameObject rock)
    {
        this.chunkWidth = chunkWidth;
        this.rockWidth = rockWidth;
        this.centre = centre;
        this.noiseThreshold = noiseThreshold;
        this.noiseScale = noiseScale;
        this.rock = rock;
    }


    public void GenerateRocks()
    {
        Vector3 startPosition = centre + Vector3.left * chunkWidth / 2 + Vector3.back * chunkWidth / 2 + Vector3.right * rockWidth / 2 + Vector3.forward * rockWidth / 2;
        int noOfRocks = (int)chunkWidth / (int)rockWidth;
     

        for (int y = 0; y < noOfRocks; y++)
        {
            for (int x = 0; x < noOfRocks; x++)
            {
                Vector3 rockPosition = startPosition + Vector3.forward * rockWidth * y + Vector3.right * rockWidth * x;
                if (noiseScale <= 0)
                {
                    noiseScale = 0.01f;
                }
                Vector2 perlinInput = new Vector2(100f + rockPosition.x, 100f + rockPosition.z) / noiseScale;
                float perlin = Mathf.PerlinNoise(perlinInput.x, perlinInput.y);

                if (perlin > noiseThreshold)
                {
                    Instantiate(rock, rockPosition, Quaternion.identity, transform);
                }
                //else
                //{
                //    // Optionally, visualize positions that don't meet the threshold
                //    Gizmos.color = Color.red; // Rocks that fail the threshold
                //    Gizmos.DrawWireCube(rockPosition, Vector3.one * rockWidth * 0.8f); // Wireframe cube for failed positions
                //}

            }
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Vector3 startPosition = centre + Vector3.left * chunkWidth/2 + Vector3.back * chunkWidth/2 + Vector3.right * rockWidth / 2 + Vector3.forward * rockWidth / 2;
    //    int noOfRocks = (int)chunkWidth / (int)rockWidth;

    //    for (int y = 0; y < noOfRocks; y++)
    //    {
    //        for (int x = 0; x < noOfRocks; x++)
    //        {
    //            Vector3 rockPosition = startPosition + Vector3.forward * rockWidth * y + Vector3.right * rockWidth * x;
    //            if (noiseScale <= 0)
    //            {
    //                noiseScale = 0.01f;
    //            }
    //            Vector2 perlinInput = new Vector2(rockPosition.x, rockPosition.z) / noiseScale;

    //            float perlin = Mathf.PerlinNoise(perlinInput.x, perlinInput.y);

    //            if (perlin > noiseThreshold)
    //            {
    //                // Draw a small cube at the position where the rock would be spawned
    //                Gizmos.color = Color.green; // Rocks that pass the threshold
    //                Gizmos.DrawCube(rockPosition, Vector3.one * rockWidth * 0.8f); // Slightly smaller than rockWidth for spacing
    //            }
    //            else
    //            {
    //                // Optionally, visualize positions that don't meet the threshold
    //                Gizmos.color = Color.red; // Rocks that fail the threshold
    //                Gizmos.DrawWireCube(rockPosition, Vector3.one * rockWidth * 0.8f); // Wireframe cube for failed positions
    //            }

    //        }
    //    }
    //}
}
