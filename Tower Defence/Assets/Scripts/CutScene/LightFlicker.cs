using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightFlicker : MonoBehaviour
{
    public float minFlickerTime, maxFlickerTime;
    public float minOffTime, maxOffTime;
    float resetTime;
    public Light light;
    float timer = 0f;
  
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > resetTime)
        {
            if (light.enabled)
            {
                light.enabled = false;
                resetTime = Random.Range(minOffTime, maxOffTime);
            }
            else
            {
                light.enabled = true;
                resetTime = Random.Range(minFlickerTime, maxFlickerTime);
            }
            timer = 0f;
        }
        
    }
}
