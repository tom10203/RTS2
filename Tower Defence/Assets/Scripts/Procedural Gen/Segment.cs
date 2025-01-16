using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    public Vector3 coordinates;
    public float width;
    public float height;

    public Vector3 CornerA { get; private set; }
    public Vector3 CornerB { get; private set; }
    public Vector3 CornerC { get; private set; }
    public Vector3 CornerD { get; private set; }
    public Vector3 CornerE { get; private set; }
    public Vector3 CornerF { get; private set; }
    public Vector3 CornerG { get; private set; }
    public Vector3 CornerH { get; private set; }
    public bool IsLeft { get; set; }
    public bool IsRight { get; set; }
    public bool IsForward { get; set; }
    public bool IsBack { get; set; }
    public bool IsTop { get; set; }

    public Vector3[] vertices { get; private set; }

    public Segment(Vector3 coordinates, float width, float height, bool left, bool back, bool right, bool forward, bool top)
    {
        this.coordinates = coordinates;
        this.width = width;
        this.IsLeft = left;
        this.IsBack = back;
        this.IsRight = right;
        this.IsForward = forward;
        this.IsTop = top;
        

        float halfWidth = width / 2;

        CornerA = coordinates + ( Vector3.forward - Vector3.right) * halfWidth;
        CornerB = coordinates + (-Vector3.forward - Vector3.right) * halfWidth;
        CornerC = coordinates + (-Vector3.forward + Vector3.right) * halfWidth;
        CornerD = coordinates + ( Vector3.forward + Vector3.right) * halfWidth;

        CornerE = CornerA + Vector3.up * height;
        CornerF = CornerB + Vector3.up * height;
        CornerG = CornerC + Vector3.up * height;
        CornerH = CornerD + Vector3.up * height;

        vertices = new Vector3[8];
        vertices[0] = CornerA;
        vertices[1] = CornerB;
        vertices[2] = CornerC;
        vertices[3] = CornerD;

        vertices[4] = CornerE;
        vertices[5] = CornerF;
        vertices[6] = CornerG;
        vertices[7] = CornerH;
    }

    public int[] TriangleData()
    {
        List<int> triangles = new List<int>();
        int[] trianglesArr;

        if (!IsBack && !IsLeft && !IsForward && !IsRight && !IsTop)
        {
            // E->H->F , H->G->F
            trianglesArr = new int[6];
            trianglesArr[0] = 0;
            trianglesArr[1] = 3;
            trianglesArr[2] = 1;
            trianglesArr[3] = 3;
            trianglesArr[4] = 2;
            trianglesArr[5] = 1;
            return trianglesArr;
        }
        if (IsLeft)
        {
            triangles.Add(0);
            triangles.Add(4);
            triangles.Add(1);
            triangles.Add(4);
            triangles.Add(5);
            triangles.Add(1);
        }
        if (IsBack)
        {
            triangles.Add(1);
            triangles.Add(5);
            triangles.Add(2);
            triangles.Add(5);
            triangles.Add(6);
            triangles.Add(2);
        }
        if (IsRight)
        {
            triangles.Add(2);
            triangles.Add(6);
            triangles.Add(3);
            triangles.Add(6);
            triangles.Add(7);
            triangles.Add(3);
        }
        if (IsForward)
        {
            triangles.Add(3);
            triangles.Add(7);
            triangles.Add(0);
            triangles.Add(7);
            triangles.Add(4);
            triangles.Add(0);
        }
        if (IsTop)
        {
            triangles.Add(4);
            triangles.Add(7);
            triangles.Add(5);
            triangles.Add(7);
            triangles.Add(6);
            triangles.Add(5);
        }

        trianglesArr = new int[triangles.Count];
        for (int i = 0; i < triangles.Count; i++)
        {
            trianglesArr[i] = triangles[i];
        }
        return trianglesArr;

    }

    public int[] LeftWallTriangleData()
    {
        // Left wall A->E->B, E->F->B
        int[] triangleData = new int[6];
        triangleData[0] = 0;
        triangleData[1] = 4;
        triangleData[2] = 1;
        triangleData[3] = 4;
        triangleData[4] = 5;
        triangleData[5] = 1;

        return triangleData;

    }

    public int[] BackWallTriangleData()
    {
        // Left wall B->F->C, F->G->C
        int[] triangleData = new int[6];
        triangleData[0] = 1;
        triangleData[1] = 5;
        triangleData[2] = 2;
        triangleData[3] = 5;
        triangleData[4] = 6;
        triangleData[5] = 2;

        return triangleData;

    }

    public int[] RightWallTriangleData()
    {
        // Left wall C->G->D, G->H->D
        int[] triangleData = new int[6];
        triangleData[0] = 2;
        triangleData[1] = 6;
        triangleData[2] = 3;
        triangleData[3] = 6;
        triangleData[4] = 7;
        triangleData[5] = 3;

        return triangleData;

    }

    public int[] ForwardWallTriangleData()
    {
        // Left wall D->H->A, H->E->A
        int[] triangleData = new int[6];
        triangleData[0] = 3;
        triangleData[1] = 7;
        triangleData[2] = 0;
        triangleData[3] = 7;
        triangleData[4] = 4;
        triangleData[5] = 0;

        return triangleData;

    }
    public int[] TopWallTriangleData()
    {
        // Left wall E->H->F, H->G->F
        int[] triangleData = new int[6];
        triangleData[0] = 4;
        triangleData[1] = 7;
        triangleData[2] = 5;
        triangleData[3] = 7;
        triangleData[4] = 6;
        triangleData[5] = 5;

        return triangleData;

    }
}
