using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static GenerateTextureTests;

public class NoiseTests
{
    // Start is called before the first frame update
    public static float[,] PerlinNoise(int width, int octaves, float lacunarity, float persistance, float scale, Vector2 offset, int seed, noiseState noiseType)
    {

        float[,] noiseMap = new float[width, width];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxVal = float.MinValue;
        float minVal = float.MaxValue;

        float noiseValue = 0;

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float frequency = 1;
                float amplitude = 1;
                float noiseHeight = 0;

                for (int z = 0; z < octaves; z++)
                {
                    float sampleX = x / scale * frequency + octaveOffsets[z].x;
                    float sampleY = y / scale * frequency + octaveOffsets[z].y;
                    //Debug.Log($"sampleX sampleY values {sampleX} {sampleY}");
                    if (noiseType == noiseState.Perlin)
                    {
                        noiseValue = Mathf.PerlinNoise(sampleX, sampleY);
                    }
                    else if (noiseType == noiseState.Cellular)
                    {
                        float2 input = new float2(sampleX, sampleY);
                        noiseValue = noise.cellular(input).x;
                    }
                    //noiseHeight += perlinValue * amplitude;
                    noiseHeight += noiseValue;
                    frequency *= lacunarity;
                    amplitude *= persistance;

                }

                if (noiseHeight > maxVal)
                {
                    maxVal = noiseHeight;
                }
                if (noiseHeight < minVal)
                {
                    minVal = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        //Debug.Log($"min, max values {minVal} {maxVal}");
        int val = 0;
        for (int i = 0; i < noiseMap.GetLength(0); i++)
        {
            for (int j = 0; j < noiseMap.GetLength(1); j++)
            {
                noiseMap[i, j] = Mathf.InverseLerp(minVal, maxVal, noiseMap[i, j]);
                if (noiseMap[i, j] > 0.5f)
                {
                    noiseMap[i, j] = 1f;
                }
                else
                {
                    noiseMap[i, j] = 0f;
                }
            }
        }

        return noiseMap;
    }
}
