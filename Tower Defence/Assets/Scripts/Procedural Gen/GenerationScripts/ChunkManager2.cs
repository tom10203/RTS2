using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager2 : MonoBehaviour
{

    [Header("Individual Chunk Variables")]
    public int chunkResolution = 3;
    public float chunkWidth = 10f;
    public Material chunkMaterial;

    [Header("Scene Variables")]
    public int noOfChunksX = 5;
    public int noOfChunksY = 2;

    [Header("Visible Chunks Variables")]
    public int chunksVisX = 4;
    public int chunksVisY = 3;

    public Transform player;
    Dictionary<Vector2, GameObject> chunks = new Dictionary<Vector2, GameObject>();
    List<GameObject> chunksVisLastFrame = new List<GameObject>();

    [Header("Chunk Rock Variables")]
    public float rockWidth = 1f;
    public float noiseScale = 1f;
    public float noiseThreshold = 0.5f;
    public GameObject rock;
    ChunkRocks generateRocks;

    Grid grid;

    private void Awake()
    {
        // This function initialises chunks based on chunk position, width, noOfChunks
        // It first generates chunks, then generates the rocks, then generates the grid of nodes
        // It first sets the chunk (with the rocks) to active so when the grid is generated, the nodes are able to detect collisions with the rock colliders. It then sets the rocks and chunks to inactive.

        
        float mapWidth = chunkWidth * noOfChunksX;
        float mapHeight = chunkWidth * noOfChunksY;

        Vector3 startPosition = transform.position + Vector3.left * mapWidth/2 + Vector3.back * mapWidth/2 + Vector3.right * chunkWidth/2 + Vector3.forward * chunkWidth/2;
        for (int y = 0; y < noOfChunksY; y++)
        {
            for (int x = 0; x < noOfChunksX; x++)
            {
                Vector3 chunkPosition = startPosition + x * Vector3.right * chunkWidth + y * Vector3.forward * chunkWidth;
                Vector2 chunksKey = new Vector2(chunkPosition.x, chunkPosition.z);
                chunks[chunksKey] = GenerateChunk(chunkPosition, chunkMaterial);         
            }
        }

        grid = GetComponent<Grid>();
        grid.GenerateGrid();


        foreach (KeyValuePair<Vector2, GameObject> entry in chunks)
        {
            GameObject chunk = entry.Value;
            chunk.SetActive(false);
        }

    }
    void Update()
    {

        for (int i = 0; i < chunksVisLastFrame.Count; i++)
        {
            chunksVisLastFrame[i].SetActive(false);
        }

        chunksVisLastFrame.Clear();

        Vector2 playerChunkPosition = GetChunkKey(player.position);

        for (int y = 0; y < chunksVisY; y++)
        {
            for (int x = 0; x < chunksVisX; x++)
            {
                Vector2 chunkKey = new Vector2(playerChunkPosition.x - chunksVisX / 2 * chunkWidth + x * chunkWidth, playerChunkPosition.y - chunksVisY / 2 * chunkWidth + y * chunkWidth);
                //print(chunkKey);
                if (chunks.ContainsKey(chunkKey))
                {
                    chunks[chunkKey].SetActive(true);
                    chunksVisLastFrame.Add(chunks[chunkKey]);
                }

            }
        }
    }

    GameObject GenerateChunk(Vector3 position, Material material)
    {
        GameObject go = new GameObject("Chunk");
        go.transform.parent = transform;
        Chunk2 chunk = go.AddComponent<Chunk2>();
        ChunkRocks chunkRocks = go.AddComponent<ChunkRocks>();
        chunkRocks.SetRockVariables(chunkWidth, rockWidth, position, noiseThreshold, noiseScale, rock);
        chunkRocks.GenerateRocks();
        chunk.SetChunkVariables(position, chunkWidth, chunkResolution, material);
        chunk.SetMaterial();
        chunk.GenerateMesh();
        go.SetActive(true);
        return go;
    }


    Vector2 GetChunkKey(Vector3 position)
    {
        float x = Mathf.Floor(position.x / chunkWidth) * chunkWidth + chunkWidth/2f;
        float y = Mathf.Floor(position.z / chunkWidth) * chunkWidth + chunkWidth/2f;

        return new Vector2(x, y);
    }

}
