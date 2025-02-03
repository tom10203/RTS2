using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk2 : MonoBehaviour
{
    public Vector3 center;
    public float width;
    public int resolution;
    public Material material;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;


    private void Awake()
    {
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<MeshFilter>();

        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }

    public void SetChunkVariables(Vector3 center, float width, int resolution, Material material)
    {
        this.center = center;
        this.width = width;
        this.resolution = resolution;
        this.material = material;
    }

    public void SetMaterial()
    {
        meshRenderer.material = material;
    }
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];


        float stepChange = width / ((float)resolution - 1f);

        int triangleIdx = 0;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++) 
            {
                int i = y * resolution + x;
                Vector3 vertexPosition = center + Vector3.left * width / 2 + Vector3.back * width / 2 + Vector3.right * x * stepChange + Vector3.forward * y * stepChange;

                vertices[i] = vertexPosition;

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triangleIdx] = i;
                    triangles[triangleIdx + 1] = i + resolution;
                    triangles[triangleIdx + 2] = i + resolution + 1;

                    triangles[triangleIdx + 3] = i;
                    triangles[triangleIdx + 4] = i + resolution + 1;
                    triangles[triangleIdx + 5] = i + 1;

                    triangleIdx += 6;

                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter.sharedMesh = mesh;
    }

}
