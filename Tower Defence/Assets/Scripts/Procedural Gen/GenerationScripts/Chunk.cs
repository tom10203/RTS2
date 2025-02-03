using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GenerateTextureTests;

public class Chunk: MonoBehaviour
{
    public Dictionary<Vector2, GameObject> segments = new Dictionary<Vector2, GameObject>();
    public GameObject rock;
    public int width;
    public GameObject chunk;
    public Vector3 worldPositionOrigin;
    public int noOfSegments;
    public int segmentWidth;
    public int segmentHeight;
    public float noiseScale;
    public float noiseThreshold;

    class ChunkNode
    {
        public bool walkable = false;
        public ChunkNode parent;
        public float hScore;
        public float gScore;
        public Vector3 worldPosition;

        public ChunkNode (bool walkable, ChunkNode parent, float hScore, float gScore, Vector3 worldPosition)
        {
            this.walkable = walkable;
            this.parent = parent;
            this.hScore = hScore;
            this.gScore = gScore;
            this.worldPosition = worldPosition;
        }
    }

    public void OnInstantiate(int segmentWidth, int segmentHeight, int noOfSegments, int rockHeight, Vector3 worldPositionOrigin, Material material, Transform parent, GameObject rock, float noiseScale, float noiseThreshold)
    {
        this.rock = rock;
        this.segmentWidth = segmentWidth;
        this.segmentHeight = segmentHeight;
        this.worldPositionOrigin = worldPositionOrigin;
        this.noOfSegments = noOfSegments;
        this.noiseScale = noiseScale;
        this.noiseThreshold = noiseThreshold;

        width = segmentWidth * noOfSegments;

        chunk = transform.gameObject;
        chunk.layer = 6;
        chunk.transform.parent = parent;
        MeshFilter meshFilter = chunk.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = chunk.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = chunk.AddComponent<MeshCollider>();

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(noOfSegments + 1) * (noOfSegments + 1)];
        int[] triangles = new int[(noOfSegments * noOfSegments) * 6];

        float startX = worldPositionOrigin.x - (float)width / 2;
        float startZ = worldPositionOrigin.z - (float)width / 2;

        int triIdx = 0;
        for (int z = 0; z < noOfSegments + 1; z++)
        {
            for (int x = 0; x < noOfSegments + 1; x++)
            {
                int i = z * (noOfSegments + 1) + x;

                float vertexX = startX + (x * segmentWidth);
                float vertexZ = startZ + (z * segmentWidth);

                Vector3 vertexPosition = new Vector3(vertexX, 0, vertexZ);
                vertices[i] = vertexPosition;

                if (z < noOfSegments && x < noOfSegments)
                {
                    triangles[triIdx] = i;
                    triangles[triIdx + 1] = i + (noOfSegments + 1);
                    triangles[triIdx + 2] = i + (noOfSegments + 1) + 1;

                    triangles[triIdx + 3] = i;
                    triangles[triIdx + 4] = i + (noOfSegments + 1) + 1;
                    triangles[triIdx + 5] = i + 1;

                    triIdx += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshRenderer.sharedMaterial = material;
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;


    }

    public void SetActiveTerrain(bool active)
    {
        chunk.SetActive(active);
    }

    public void PopulateChunkWithRocks()
    {
        int startZ = (int)worldPositionOrigin.z - Mathf.RoundToInt(noOfSegments / 2) * segmentWidth;
        int startX = (int)worldPositionOrigin.x - Mathf.RoundToInt(noOfSegments / 2) * segmentWidth;

        for (int z = startZ; z < startZ + noOfSegments * segmentWidth; z += segmentWidth)
        {
            for (int x = startX; x < startX + noOfSegments * segmentWidth; x += segmentWidth)
            {
                float keyX = (float)x * segmentWidth;
                Vector2 key = new Vector2(x, z);
                float perlinValue = GetPerlinValue(key, noiseScale);
                Vector2 rockWorldPosition = new Vector2(x * segmentWidth, z * segmentWidth);
                Vector3 rockPosition = new Vector3(x, 0, z);
                bool rockActive = true;
                if (perlinValue > noiseThreshold)
                {
                    InstantiateRock(rockPosition, rockActive);
                }
                
            }
        }
    }

    void InstantiateRock(Vector3 position, bool isActive)
    {
        GameObject go = Instantiate(rock, chunk.transform);
        go.transform.localScale = new Vector3(segmentWidth, segmentHeight, segmentWidth);
        go.transform.position = new Vector3(position.x, segmentHeight * .5f, position.z);
        go.transform.parent = chunk.transform;
        go.SetActive(isActive);

    }

    float GetPerlinValue(Vector2 position, float scale)
    {
        float perlinX = position.x / (float)segmentWidth;
        float perlinY = position.y / (float)segmentWidth;
        float perlinValue = Mathf.PerlinNoise((1000 + perlinX)/ scale, (1000 + perlinY)/ scale);
        //Debug.Log($"position, {position} ,perlinX, perlinY {perlinX}, {perlinY}, perlinValue {perlinValue}");
        return perlinValue;

    }

    public void GenerateChunkNodes()
    {

    }
}
