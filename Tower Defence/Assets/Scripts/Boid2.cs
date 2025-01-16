using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid2 : MonoBehaviour
{
    public Transform target;

    public int steps;
    public float rayLength;
    Vector3[] directions;

    public Vector3 direction;

    int i = 0;

    void Update()
    {


        PopulateDirectionsArray();
        FindPath();


        Debug.DrawRay(transform.position, direction * rayLength, Color.green);

    }

    void CalculateTargetDirectionAngle(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
    }

    void PopulateDirectionsArray()
    {
        directions = new Vector3[steps];

        Vector3 targetDir = (target.position - transform.position).normalized;
        Debug.DrawRay(transform.position, targetDir, Color.red);

        float angle = Vector3.SignedAngle(transform.forward, targetDir, transform.up);
        float radians = Mathf.Deg2Rad * angle;

        float percent = 2 * Mathf.PI / steps;

        for (int i = 0; i < steps/2; i += 2)
        {

            directions[i] = CalculateAngle(radians, i);
            directions[i+1] = CalculateAngle(radians, i*-1);

        }

    }

    Vector3 CalculateAngle(float angle, int i)
    {
        float percent = 2 * Mathf.PI / steps;
        float radian = angle + i * percent % (2 * Mathf.PI);

        float x = Mathf.Sin(radian);
        float y = Mathf.Cos(radian);

        Vector3 dir = new Vector3(x, 0, y);
        Vector3 localDir = transform.TransformDirection(dir);
        Debug.DrawRay(transform.position, localDir, Color.red);
        return localDir;
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

}
