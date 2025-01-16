using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoiseReference
{
    static float ReturnNoiseValue(Vector2 coordines, float scale)
    {
        float inputX = coordines.x * scale;
        float inputY = coordines.y * scale;
        float noiseValue = Mathf.PerlinNoise(inputX, inputY);   
        return -1f;
    }

}
