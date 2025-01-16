using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public float interactionDistance = 5.0f;
    public float boxWidth = 2.0f;
    public float boxHeight = 2.0f;
    Vector3 boxCenter;
    Vector3 boxHalfExtents;

    public LayerMask interactableLayer;
    public bool enableInteractions = false;

    public float enableInteractionBoxWidth = 0.5f;
    public float enableInteractionBoxHeight = 0.5f;
    public float enableInteractionBoxDistance = 0.5f;
    Vector3 boxCenter1;
    Vector3 boxHalfExtents1;

    Collider[] hitColliders;

    public float hitStrength;

    float animationTimer = 1f;
    bool resetTimer = false;
    public float animationLength = 1f;

    int frameCount = 0;

    private void Start()
    {
        boxCenter = transform.position + transform.forward * interactionDistance * 0.5f;
        boxHalfExtents = new Vector3(boxWidth / 2, boxHeight / 2, interactionDistance / 2);

        boxCenter1 = transform.position + transform.forward * enableInteractionBoxDistance * 0.5f;
        boxHalfExtents1 = new Vector3(enableInteractionBoxWidth / 2, enableInteractionBoxHeight / 2, enableInteractionBoxDistance / 2);
    }
    private void Update()
    {
        animationTimer += Time.deltaTime;

        Collider[] colliders = Physics.OverlapBox(boxCenter1, boxHalfExtents1, transform.rotation, interactableLayer);
        if (colliders.Length == 0 )
        {
            enableInteractions = false;
        }
        else
        {
            enableInteractions = true;
        }


        if (enableInteractions)
        {
            //print($"interactions enabled");

            if (animationTimer >= animationLength)
            {
                hitColliders = Physics.OverlapBox(boxCenter, boxHalfExtents, transform.rotation, interactableLayer);
                animationTimer = 0;
            }

            if (hitColliders.Length > 0)
            { 
                foreach (Collider collider in hitColliders)
                {

                    Rock rock = collider.GetComponent<Rock>();

                    resetTimer = true;


                    float distanceFromRock = (collider.transform.position - transform.position).magnitude;
                    float damageToRock = (1 / distanceFromRock) * hitStrength;

                    //print($"damageToRock {damageToRock}");
                    rock.AdjustHealth(damageToRock);

                    rock.AnimateRock(animationTimer);
                    
                }

                
            }

 
        }

        frameCount += 1;
    }

    private void OnDrawGizmos()
    {
        // Set the color for the Gizmos
        Gizmos.color = Color.cyan;

        // Calculate the center and half-extents of the box in world space
        boxCenter = transform.position + transform.forward * interactionDistance * 0.5f;
        boxHalfExtents = new Vector3(boxWidth / 2, boxHeight / 2, interactionDistance / 2);

        boxCenter1 = transform.position + transform.forward * enableInteractionBoxDistance * 0.5f;
        boxHalfExtents1 = new Vector3(enableInteractionBoxWidth / 2, enableInteractionBoxHeight / 2, enableInteractionBoxDistance / 2);

        // Draw a wireframe cube that represents the OverlapBox area
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxHalfExtents * 2);

        Gizmos.matrix = Matrix4x4.TRS(boxCenter1, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxHalfExtents1 * 2);

        // Optionally, reset the Gizmos matrix to default
        Gizmos.matrix = Matrix4x4.identity;
    }
}