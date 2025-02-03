using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public float chunkWidth = 10f;
    public int noOfChunks = 10;
    public float nodeWidth = 0.5f;
    public float worldSize;
    public Dictionary<Vector2, Node> nodes = new Dictionary<Vector2, Node>();
    public Transform player;
    public Transform enemy;
    public float playerDistanceThreshold = 1f;
    Vector3 lastPLayerPosition;
    List<Node> pathToTarget = new List<Node>();
  

    int noOfNodes;

    private void Awake()
    {
        worldSize = chunkWidth * (float)noOfChunks - nodeWidth * 2; //minusing nodeWidth * 2, as nodes start and end one nodeWidth away from each end
        float tempNoOfNodes = worldSize / nodeWidth;
        noOfNodes = (int)tempNoOfNodes;
        lastPLayerPosition = player.position;

    }

    private void Update()
    {
        //if (Vector3.Distance(lastPLayerPosition, player.position) > playerDistanceThreshold)
        //{
        //    print($"Calling AStar function");
        //    lastPLayerPosition = player.position;
        //    pathToTarget = AStar(enemy.position, player.position);


        //}

        //foreach (Node node in pathToTarget)
        //{
        //    Debug.DrawRay(node.position, Vector3.up, Color.green);
        //}
        //if (nodes.Count > 0)
        //{
        //    Vector3 nodeKey = WorldPositionToNodeKey(player.position);
        //    if (nodes.ContainsKey(nodeKey))
        //    {
        //        Vector3 nodePosition = nodes[nodeKey].position;
        //        if (nodes[nodeKey].walkable)
        //        {
        //            Debug.DrawRay(nodePosition, Vector3.up, Color.green);
        //        }
        //        else
        //        {
        //            Debug.DrawRay(nodePosition, Vector3.up, Color.red);
        //        }

        //    }
        //    else
        //    {
        //        print($"nodeKey not in nodes");
        //    }
        //}
        //else
        //{
        //    print($"nodes.Count == 0");
        //}
        Vector3 nodeKey = WorldPositionToNodeKey(player.position);
        Node node = nodes[nodeKey];
        if (node.walkable)
        {
            Debug.DrawRay(node.position, Vector3.up, Color.green);
        }
        else
        {
            Debug.DrawRay(node.position, Vector3.up, Color.red);
        }

    }
    public void GenerateGrid()
    {
        float mapWidth = chunkWidth * (float)noOfChunks;
        if (nodeWidth <= 0)
        {
            nodeWidth = 0.1f;
        }
        float noOfNodes = mapWidth / nodeWidth;
        noOfNodes = (int)noOfNodes;

        Vector3 startPosition = transform.position + Vector3.left * mapWidth/2 + Vector3.back * mapWidth/2 + Vector3.right * nodeWidth / 2 + Vector3.forward * nodeWidth/2;

        for (int y = 0; y < noOfNodes; y++)
        {
            for (int x = 0; x < noOfNodes; x++)
            {
                
                Vector3 nodePosition = startPosition + (Vector3.right * x + Vector3.forward * y) * nodeWidth;

                Node node = new Node();
                node.position = nodePosition;
                node.gridX = x;
                node.gridY = y;

                Vector2 nodeKey = new Vector2(x, y);
                if (!Physics.CheckSphere(nodePosition, nodeWidth, 1<<7))
                {
                    node.walkable = true;
                }
                else
                {
                    node.walkable = false;
                }
                nodes[nodeKey] = node;
            }
        }
    }

    Vector2 WorldPositionToNodeKey(Vector3 position)
    {
        float percentX = (position.x + worldSize / 2) / worldSize;
        float percentY = (position.z + worldSize / 2) / worldSize;
        int x = Mathf.RoundToInt(percentX * noOfNodes);
        int y = Mathf.RoundToInt(percentY * noOfNodes);
        return new Vector2(x, y);
    }

    public List<Node> AStar(Vector3 current, Vector3 target)
    {

        //Dictionary<Vector2, Node> nodesCopy = new Dictionary<Vector2, Node>(nodes);
        int count = 0;

        Node startNode = nodes[WorldPositionToNodeKey(current)];
        Node endNode = nodes[WorldPositionToNodeKey(target)];
        startNode.start = true;


        MinHeap2 openSet = new MinHeap2();
        openSet.AddNode(startNode);
        HashSet<Node> closed = new HashSet<Node>();
        HashSet<Node> open = new HashSet<Node>();

        while (openSet.length > 0 && count < 3000)
        {
            Node currentNode = openSet.ReturnMinElement();
            closed.Add(currentNode);


            if (currentNode == endNode)
            {
                List<Node> pathToTarget = GetPathToTarget(currentNode);
                
                foreach ( Node node in closed)
                {
                    node.parent = null;
                    node.gScore = 0;
                    node.hScore = 0;
                }
                foreach ( Node node in openSet.heap)
                {
                    node.parent = null;
                    node.gScore = 0;
                    node.hScore = 0;
                }
                print($"end node reached, while loop itteration {count}");
                return pathToTarget;
            }

            

            List<Node> neighbors = GetNeighbors(currentNode, nodes);
            for (int i = 0;i < neighbors.Count; i++)
            {
                Node neighbor = neighbors[i];

                if (closed.Contains(neighbor))
                {
                    continue;
                }

                int distanceToNeighbor = currentNode.gScore + GetDistanceBetweenNodes(currentNode, neighbor);
                if (distanceToNeighbor < neighbor.gScore || !openSet.Contains(neighbor))
                {
                    neighbor.gScore = distanceToNeighbor;
                    neighbor.hScore = GetDistanceBetweenNodes(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.AddNode(neighbor);
                    }
                }
            }
            count++;
        }
        print($"AStar could not fin path {count}");
        return new List<Node>();

    }

    List<Node> GetPathToTarget(Node node)
    {
        List<Node> path = new List<Node>();
        path.Add(node);
        while (!node.start)
        {
            path.Add(node.parent);
            node = node.parent;
        }
        path.Reverse();
        print($"GetPathToTarget path.Count {path.Count}");
        return path;
    }

    List<Node> GetNeighbors(Node node, Dictionary<Vector2, Node> nodes)
    {
        List<Node> neighbors = new List<Node>();
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                int xx = node.gridX + x;
                int yy = node.gridY + y;
                if ((xx >=0 && x < noOfNodes) && (yy >= 0 && yy < noOfNodes) && (xx != 0 && yy != 0))
                {
                    Node neighbor = nodes[new Vector2(xx, yy)];
                    if (neighbor.walkable)
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }
        }
        return neighbors;
    }

    int GetDistanceBetweenNodes(Node node1, Node node2)
    {
        int xDist = Mathf.Abs(node1.gridX - node2.gridX);
        int yDist = Mathf.Abs(node1.gridY - node2.gridY);

        if (xDist > yDist)
        {
            return 14*yDist + 10*(xDist - yDist);
        }

        return 14*xDist + 10 *(yDist - xDist);
    }
}




