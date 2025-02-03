using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class ChunkManager : MonoBehaviour
{
    public int segmentWidth;
    public int segmentHeight;
    public int SqrtSegmentCount;

    public float noiseScale;
    public float noiseThreshold;

    public Material material;

    int chunkWidth;
    public Transform player;

    Dictionary<Vector2, Chunk> generatedChunks = new Dictionary<Vector2, Chunk>();
    List<Chunk> chunksVisLastFrame = new List<Chunk>();

    public GameObject rock;

    public int maxViewDst = 50;
    int maxChunksVisible;

    public int noOfChunksX;
    public int noOfChunksY;

    private void Start()
    {
        for (int y = 0; y < noOfChunksY; y++)
        {
            for (int x = 0; x < noOfChunksX; x++)
            {
                Vector3 chunkWorldPos = transform.position + Vector3.right * (x * chunkWidth - (noOfChunksX * chunkWidth)/2) + Vector3.forward * (y * chunkWidth - (noOfChunksY * chunkWidth)/2);
                GameObject chunkGO = new GameObject("chunk");
                Chunk chunk = chunkGO.AddComponent<Chunk>();
                chunk.OnInstantiate(segmentWidth, segmentHeight, SqrtSegmentCount, segmentHeight, chunkWorldPos, material, transform, rock, noiseScale, noiseThreshold);
            }
        }

        chunkWidth = SqrtSegmentCount * segmentWidth;
        maxChunksVisible = maxViewDst / chunkWidth;
    }

    private void Update()
    {
        //for (int i = 0; i < chunksVisLastFrame.Count; i++)
        //{
        //    chunksVisLastFrame[i].SetActiveTerrain(false);
        //}
        //chunksVisLastFrame.Clear();


        //int currentChunkX = Mathf.RoundToInt(player.position.x / chunkWidth);
        //int currentChunkZ = Mathf.RoundToInt(player.position.z / chunkWidth);

        //int startingX = currentChunkX - Mathf.RoundToInt(maxChunksVisible / 2);
        //int startingZ = currentChunkZ - Mathf.RoundToInt(maxChunksVisible / 2);

        ////print($"startingX, startingZ {startingX} {startingZ}");

        //for (int z = startingZ; z < startingZ + maxChunksVisible; z++)
        //{
        //    for (int x = startingX; x < startingX + maxChunksVisible; x++)
        //    {
        //        Vector2 chunkPosition = new Vector2(x, z);
        //        if (generatedChunks.ContainsKey(chunkPosition))
        //        {
        //            generatedChunks[chunkPosition].SetActiveTerrain(true);
        //            chunksVisLastFrame.Add(generatedChunks[chunkPosition]);
        //        }
        //        else
        //        {
        //            GameObject chunkGO = new GameObject("chunk");
        //            Chunk chunk = chunkGO.AddComponent<Chunk>();
        //            chunk.OnInstantiate(segmentWidth, segmentHeight, SqrtSegmentCount, segmentHeight, new Vector3(chunkPosition.x * chunkWidth, 0, chunkPosition.y * chunkWidth), material, transform, rock, noiseScale, noiseThreshold);
        //            chunk.PopulateChunkWithRocks();
        //            generatedChunks[chunkPosition] = chunk;
        //        }
        //    }

        //}
    }
  
}
