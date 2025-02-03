using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node 
{
    // Start is called before the first frame update

    public bool walkable;
    public Node parent;
    public int gScore;
    public int hScore;
    public Vector3 position;
    public bool start = false;
    public int heapIndex;

    public int gridX, gridY;

    public int fScore { get { return gScore + hScore; } }
    
}
