using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetDirection;

    Boid2 Boid2;

    public float moveSpeed;
    public float RotationSpeed;
    void Start()
    {
        Boid2 = GetComponent<Boid2>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move(); 
    }

    private void Move()
    {
        transform.Translate(Boid2.direction * Time.deltaTime * moveSpeed, Space.World);
    }

    void Rotate()
    {
        transform.forward = Boid2.direction;
    }
}
