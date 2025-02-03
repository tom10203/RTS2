using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boid2 : MonoBehaviour
{
    public Transform target;
    Vector3 lastTargetPosition;
    public BoidsManager manager;
    public int steps = 50;
    public float rayLength = 7f;
    Vector3[] directions;

    public Vector3 direction;
    Vector3 targetDir;
    Grid grid;
    List<Node> nodes = new List<Node>();

    int i = 0;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastTargetPosition = target.position;
        manager = transform.parent.GetComponent<BoidsManager>();
        grid = FindFirstObjectByType<Grid>();

    }
    void Update()
    {

        targetDir = target.position - transform.position;
        if (Physics.Raycast(transform.position, targetDir, targetDir.magnitude, 1 << 7))
        {
            if (nodes.Count == 0 || CheckTargetHasMoved())
            {
                nodes = grid.AStar(transform.position, target.position);
            }
            MoveAlongNodePath();
        }

        AdjustTargetDir();
        PopulateDirectionsArray();
        FindPath();

        
        Debug.DrawRay(transform.position, direction * rayLength, Color.green);

    }

    bool CheckTargetHasMoved()
    {
        if (Vector3.Distance(target.position, lastTargetPosition) > 3f)
        {
            lastTargetPosition = target.position;
            return true;
        }
        return false;
    }

    void PopulateDirectionsArray()
    {
        // Create multiple vectors from the rotation of a vector pointing at the target. These are used as optional direction vectors if another object is in the way of the start target direction (towards the player)
        // Each new rotated vector is done sequentially. i.e the next two vectors are one step change either side of the target direction.
        directions = new Vector3[steps];

        Quaternion rotationVector = Quaternion.FromToRotation(Vector3.forward, targetDir);

        Debug.DrawRay(transform.position, targetDir, Color.red);

        float percent = 2 * Mathf.PI / steps;

        for (int i = 0; i < steps / 2; i += 2)
        {

            directions[i] = rotationVector * CalculateAngle(i);
            directions[i + 1] = rotationVector * CalculateAngle(-i);

            Debug.DrawRay(transform.position, rotationVector * CalculateAngle(i), Color.yellow);
            Debug.DrawRay(transform.position, rotationVector * CalculateAngle(-i), Color.yellow);

        }

    }

    Vector3 CalculateAngle(int i)
    {
        float percent = 2 * Mathf.PI / steps;
        float radian = i * percent;

        float x = Mathf.Sin(radian);
        float y = Mathf.Cos(radian);

        Vector3 dir = new Vector3(x, 0, y);
        return dir;
    }

    void FindPath()
    {

        float maxDist = float.MinValue;
        Vector3 tempDir = Vector3.zero;
        for (int i = 0; i < directions.Length; i++)
        {
            bool raycast = Physics.Raycast(transform.position, directions[i], out RaycastHit hit, rayLength);
            if (!raycast)
            {
                direction = directions[i];
                return;
            }
            else
            {
                if (hit.distance > maxDist)
                {
                    maxDist = hit.distance;
                    direction = directions[i];
                }
            }
        }
    }


    void MoveAlongNodePath()
    {
        foreach (Node temp in nodes)
        {
            Debug.DrawRay(temp.position, Vector3.up, Color.red);
        }
        for (int i = nodes.Count - 1; i >= 0; i--)
        {
            Node node = nodes[i];
            Vector3 targetPoint = new Vector3(node.position.x, transform.position.y, node.position.z);
            if (Vector3.Distance(target.position, targetPoint) < Vector3.Distance(target.position, transform.position))
            {
                targetDir = (targetPoint - transform.position).normalized;
            }
        }
    }

    void AdjustTargetDir()
    {
        //targetDir = targetDir + new Vector3(manager.averagePos.x, transform.position.y, manager.averagePos.z);
        targetDir += new Vector3(manager.averagePos.x, transform.position.y, manager.averagePos.z);
        targetDir.Normalize();
    }

}
