using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTextureTests : MonoBehaviour
{
    public Renderer textureRender;

    public int resX;
    public float scale;
    public bool autoUpdate = false;
    public int octaves = 3;
    public float lacunarity = 1f;
    public float persistance = 1f;
    public int seed;
    public Vector2 offset;

    public enum noiseState
    {
        Perlin,
        Cellular
    }

    public noiseState noiseType = noiseState.Perlin;

    public void generateTexture()
    {
        float[,] noiseMap = NoiseTests.PerlinNoise(resX, octaves, lacunarity, persistance, scale, offset, seed, noiseType);
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }
}
