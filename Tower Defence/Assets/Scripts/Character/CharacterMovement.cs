using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    // Angular speed in radians per sec.
    [Header ("Turning Variable Declaration")]
    public float turningSpeed = 1.0f;
    Vector3 targetDirection = Vector3.forward;

    [Header ("Running Variable Declaration")]
    public float maxSpeed;

    [Header("Height Variable Declaration")]
    [SerializeField] public float playerHeight;
    [SerializeField] AnimationCurve fallingSpeedCurve;
    public float fallingSpeed;

    [SerializeField] AnimationCurve jumpSpeed;
    public float jumpHeight;
        
        

    public Vector3 SetTargetDirection(float input)
    {
        Vector3 forward = transform.forward;
        float turningAngle = input * turningSpeed;
        targetDirection = Quaternion.Euler(0, turningAngle, 0) * forward;
        return targetDirection;
    }

    public void Turn(Vector3 targetDirection)
    {
        float singleStep = turningSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void MoveForwardBack(float input)
    {
        float runningSpeed = Mathf.Lerp(0, maxSpeed, Mathf.Abs(input));
        float sign = input > 0 ? 1 : -1;    
        Vector3 desiredPosition = transform.position + transform.forward * sign * runningSpeed * Time.deltaTime;
        Debug.DrawRay(desiredPosition, Vector3.up, Color.red);
        Collider[] newPositionColliders = Physics.OverlapSphere(desiredPosition, .5f, 1 << 7);
        if (newPositionColliders.Length > 0)
        {
            //print($"rock in the way, cant move player");
            return;
        }
        else
        {
            transform.position = desiredPosition;
        }
        
    }

    public void SetHeight(float t)
    {
        float fallingSpeedEvaluate = fallingSpeedCurve.Evaluate(t);
        transform.position += -Vector3.up * fallingSpeedEvaluate * fallingSpeed * Time.deltaTime;
    }

    public void Jump(float t)
    {
        float jumpHeightEvaluate = jumpSpeed.Evaluate(t);
        transform.position += Vector3.up * jumpHeightEvaluate * jumpHeight * Time.deltaTime;
    }
    
}
