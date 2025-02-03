using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMech : MonoBehaviour
{
    public float animationLengthInSeconds = 1.6333f;
    public float distanceMovedForAnimation = 5.2f; //These values are from MechWalk script;
    float timer;
    bool walk = false;
    Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (walk)
        {
            print($"mech should be walking");
            timer += Time.deltaTime;
            if (timer > 1.6333f)
            {
                timer = timer - 1.6333f;
                lastPosition = lastPosition - Vector3.forward * 5.2f;
            }
            EvaluateMovement();

        }
        else
        {
            lastPosition = transform.position;
            timer = 0f;
        }
    }

    void EvaluateMovement()
    {
        float percent = timer / 1.6333f;
        transform.position = lastPosition - Vector3.forward * 5.2f * percent;
    }

    public void ToggleWalk()
    {
        walk = !walk;
    }
}
