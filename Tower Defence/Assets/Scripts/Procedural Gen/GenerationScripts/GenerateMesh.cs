using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMesh : MonoBehaviour
{
    Material mat;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    MeshRenderer meshRenderer;
    public float threshhold = 0.55f;
    public float heightOffset = 5f;

    //[ExecuteInEditMode]
    public void ReferenceComponenets(Material material)
    {
        //print($"Awake function called");
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        mat = material;
    }


    public void generateMesh(int width, int height, float[,] noise)
    {
        Mesh mesh = new Mesh();
        mesh.name = "mesh";
        int noiseWidth = noise.GetLength(0);
        int noiseHeight = noise.GetLength(1);
        float xOffset = width / ((float)noiseWidth - 1);
        float zOffset = height / ((float)noiseHeight - 1);
        //print($"x, y Offset {xOffset} {zOffset}");
        //print($"xoffset * noisewidth {xOffset * noiseWidth} {zOffset * noiseHeight}");

        Vector3[] vertices = new Vector3[noiseWidth * noiseHeight];
        int[] triangles = new int[(noiseWidth - 1) * (noiseHeight - 1) * 6];

        int i = 0;
        int triIdx = 0;
        for (int z = 0; z < noiseHeight; z++)
        {
            for (int x = 0; x < noiseWidth; x++)
            {
                i = z * noiseWidth + x;
                if (i > vertices.Length)
                {
                    //Debug.Log("i > vertices Length");
                }
                
                float inputX = -width / 2 + (x * xOffset);
                float inputZ = height / 2 - (z * zOffset);

                if (x == noiseWidth - 1)
                {
                    //print($"inputX {inputX})");
                }
                float noiseValue = noise[x, z] > threshhold ? heightOffset : 0;
                Vector3 vertex = new Vector3(inputX, 1 + noiseValue, inputZ);
                vertices[i] = vertex;

                if (z < noiseHeight -1 && x < noiseWidth -1)
                {
                    triangles[triIdx] = i;
                    triangles[triIdx + 1] = i + 1;
                    triangles[triIdx + 2] = i + noiseWidth + 1;

                    triangles[triIdx + 3] = i;
                    triangles[triIdx + 4] = i + noiseWidth + 1;
                    triangles[triIdx + 5] = i + noiseWidth;
                    
                    triIdx += 6;
                    if (triIdx > triangles.Length)
                    {
                        //Debug.Log($"triIdx > triangles.Length {triIdx}");
                    }
                }

            }
        }
        
        //print($"vertices len {vertices.Length}");
        //print($"i {i}");

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


        meshRenderer.sharedMaterial = mat;
        meshCollider.sharedMesh = mesh;
        meshFilter.mesh = mesh;

    }
}
