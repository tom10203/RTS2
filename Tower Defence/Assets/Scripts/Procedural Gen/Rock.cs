using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Rock: MonoBehaviour
{
    public Transform meshPivot;

    public Vector2 position;
    public bool isActive = true;
    public float health = 10f;

    public float rockAnimationLength;
    public float rockAnimationScale;


    public void AdjustHealth(float damage)
    {
        health -= damage;
        if (health < 0 )
        {
            transform.gameObject.SetActive( false );
        }
    }

    public void AnimateRock(float t)
    {
        float radians = Mathf.Deg2Rad * t;
        float sine = Mathf.Sin( radians * 180 );
        //print($"sine value {sine}");
        meshPivot.localScale = Vector3.one * (1 - rockAnimationScale * sine);
    }
   
}
