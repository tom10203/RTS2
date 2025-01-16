using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Transform player;
    Vector3 desiredDir;

    public float moveSpeed;

    public float maxAngle;
    public int noOfRays;
    public float rayDist;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 transformToPlayer = (player.position - transform.position).normalized;

        desiredDir = CalculateDirection(transformToPlayer);






        MoveTransform();

    }

    void MoveTransform()
    {
        transform.Translate(desiredDir.normalized * moveSpeed * Time.deltaTime);
    }

    void Rotate()
    {

        desiredDir.Normalize();

        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, desiredDir) * transform.rotation;

        // Apply the rotation to the GameObject.
        transform.rotation = targetRotation;
    }

    void VisualiseDirections(Vector3 desiredDir)
    {
        float stepChange = maxAngle / (float)noOfRays;

        for (int i = 0; i < noOfRays; i++)
        {
            float angleToRotate = i * stepChange;

            for (int j = 0; j < 2; j++)
            {
                int sign = j == 0 ? 1 : -1;
                Vector3 dir = Quaternion.Euler(0, angleToRotate * sign, 0) * desiredDir;
                Color rayColor = new Color();
                if (dir == desiredDir)
                {
                    rayColor = Color.red;
                }
                else
                {
                    rayColor = Color.yellow;
                }
                Debug.DrawRay(transform.position, dir * rayDist, rayColor);
            }
        }
    }

    Vector3 CalculateDirection(Vector3 desiredDir)
    {
        float stepChange = maxAngle / (float)noOfRays;
        float maxDist = float.MinValue;
        Vector3 returnDir = Vector3.zero;

        for (int i = 0; i < noOfRays; i++)
        {
            float angleToRotate = i * stepChange;

            for (int j = 0; j < 2; j++)
            {
                int sign = j == 0 ? 1 : -1;
                Vector3 dir = Quaternion.Euler(0, angleToRotate * sign, 0) * desiredDir;

                RaycastHit hit;
                bool raycast = Physics.Raycast(transform.position, dir, out hit, rayDist);

                if (!raycast)
                {
                    return dir;
                }
                else
                {
                    if (hit.distance > maxDist)
                    {
                        maxDist = hit.distance;
                        returnDir = dir;
                    }
                }
            }
        }

        return returnDir;
    }

    void GetRayCastInfo(Vector3 dir)
    {
        RaycastHit hit;
        bool raycast = Physics.Raycast(transform.position, dir, out hit, rayDist);

    }
}
