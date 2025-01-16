using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateNoise
{
    //public static float[,] generateNoise(int startX, int startY, int mapWidth, int mapHeight, float scale)
    //{
    //    float[,] noiseMap = new float[mapWidth, mapHeight];

    //    if (scale <= 0)
    //    {
    //        scale = 0.0001f;
    //    }

    //    for (int y = 0; y < mapHeight; y++)
    //    {
    //        for (int x = 0; x < mapWidth; x++)
    //        {
    //            float sampleX = (startX + x) * scale;
    //            float sampleY = (startY - y) * scale;

    //            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
    //            noiseMap[x, y] = perlinValue;
    //        }
    //    }

    //    return noiseMap;
    //}

    public static float[,] generateNoise(int startX, int startY, int mapWidth, int mapHeight, float scale)
    {
        int width = Mathf.RoundToInt(mapWidth / scale);
        int height = Mathf.RoundToInt(mapHeight / scale);
        //Debug.Log($"noise map: scale, width, height {scale}, {width}, {height}");

        float[,] noiseMap = new float[width, height];

        //if (scale <= 0)
        //{
        //    scale = 0.0001f;
        //}

        for (int y = 0; y < height; y ++)
        {
            for (int x = 0; x < width; x++)
            {
                float sampleX = (startX + (x * scale)) * 0.03f;
                float sampleY = (startY - (y * scale)) * 0.03f;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
