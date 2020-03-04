using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProceduralWorld
{
    public GenerationType genType;

    public float maxHeight = 1f;
    public float minHeight = 0f;

    public int gridSize = 5;
    [Tooltip("The lower the number the more difference in height")]
    public float detail = 5.0f;
    public int seed;

    public float[,] heights; 

    public ProceduralWorld(float minHeight, float maxHeight, int gridSize, float detail, int seed, GenerationType genType)
    {
        Debug.Log("I got here");

        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.gridSize = gridSize;
        this.detail = detail;
        this.seed = seed;
        this.genType = genType;

        heights = new float[gridSize, gridSize];

        Generate();
    }

    public void Generate()
    {
        Debug.Log("Generate");
        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int z = 0; z < heights.GetLength(1); z++)
            {
                float randomHeight = 0;

                switch (genType)
                {
                    case GenerationType.Random:
                        randomHeight = UnityEngine.Random.Range(minHeight, maxHeight) / detail;
                        break;
                    case GenerationType.Perlin:
                        float perlinX = x / detail + ProceduralGenerator.instance.GetPerlinSeed();
                        float perlinZ = z / detail + ProceduralGenerator.instance.GetPerlinSeed();
                        randomHeight = (Mathf.PerlinNoise(perlinX, perlinZ) - minHeight) * maxHeight;
                        break;
                    case GenerationType.cosine:
                        randomHeight = Mathf.Cos(x + z) / detail * maxHeight;
                        break;
                    default:
                        Debug.LogError("Switch case broke");
                        break;
                }

                heights[x, z] = randomHeight;
            }
        }

    }
}

public enum GenerationType
{
    Random,
    Perlin,
    cosine,
    wave
}