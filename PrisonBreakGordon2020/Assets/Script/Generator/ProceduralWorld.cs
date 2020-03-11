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
    public float rockprobability;
    public int seed;

    public float[,] heights;
    [SerializeField]
    public GameObject[] rockPrefabs;
    public List<Vector3Int> rockData = new List<Vector3Int>();

    public ProceduralWorld(float minHeight, float maxHeight, int gridSize, float detail, int seed, GenerationType genType, GameObject[] rockPrefabs, float rockprobability)
    {
        Debug.Log("I got here");

        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.gridSize = gridSize;
        this.detail = detail;
        this.seed = seed;
        this.genType = genType;
        this.rockPrefabs = rockPrefabs;
        this.rockData = new List<Vector3Int>();
        this.rockprobability = rockprobability;

        heights = new float[gridSize, gridSize];

        Generate();
    }

    public void Generate()
    {
        Debug.Log("0");
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
                        float seed = ProceduralGenerator.instance.GetPerlinSeed();
                        float perlinX = (float)x / detail + seed;
                        float perlinZ = (float)z / detail + seed;
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

                float rockBand = UnityEngine.Random.Range(0.0f, 1.0f);

                if(rockBand > rockprobability)
                {
                    int t = UnityEngine.Random.Range(0, rockPrefabs.Length);
                    Vector3Int rock = new Vector3Int(x, z, t);
                    rockData.Add(rock);
                }
            }
        }

        
    }


}

public enum GenerationType
{
    Random,
    Perlin,
    cosine,
}