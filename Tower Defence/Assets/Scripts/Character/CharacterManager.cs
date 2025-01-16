using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Transform spawnPoint;

    CharacterMovement characterMovement;

    Vector3 turningVector;

    float fallingTime = 0f;
    public float fallingBuffer = 2f;

    private bool jumping = false;
    private bool canJump = true;
   
    float jumpTime = 0f;
    public float jumpBuffer = 1f;
    


    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        if (horizontalInput != 0)
        {
            turningVector = characterMovement.SetTargetDirection(horizontalInput);
        }
        else
        {
            turningVector = transform.forward;
        }

        if (verticalInput != 0)
        {
            //timeSinceVerticalButtonPressed += Time.deltaTime;
            characterMovement.MoveForwardBack(verticalInput);
        }
        else
        {
            //timeSinceVerticalButtonPressed = 0f;
        }

        //if (Input.GetKeyDown(KeyCode.Space) && canJump)
        //{
        //    jumping = true;
        //    canJump = false;
        //}

        //if (jumping)
        //{
        //    jumpTime += Time.fixedDeltaTime / jumpBuffer;
        //    characterMovement.Jump(jumpTime);

        //    if (jumpTime > 1)
        //    {
        //        jumping = false;
        //        jumpTime = 0f;
        //    }
        //}
        //else
        //{

        //    if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit))
        //    {
        //        if (hit.distance > characterMovement.playerHeight)
        //        {

        //            fallingTime += Time.deltaTime / fallingBuffer;
        //            fallingTime = Mathf.Clamp(fallingTime, 0, 1);

        //        }
        //        else
        //        {
        //            jumping = false;
        //            canJump = true;
        //            fallingTime = 0f;
        //        }
        //        // TO DO - Add logic if player is less than player height
        //    }
        //    else
        //    {
        //        //Debug.Log("Noraycast hit");
        //        //Debug.Break();
        //    }
        //    characterMovement.SetHeight(fallingTime);

        //}

        characterMovement.Turn(turningVector);
        

        //RaycastHit hit = Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, 1000f);

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = spawnPoint.position;
        }
    }
}
