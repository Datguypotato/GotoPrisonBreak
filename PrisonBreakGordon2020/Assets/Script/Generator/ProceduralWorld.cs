﻿using System.Collections;
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
    public Vector2Int portalData;

    //public GameObject raftPrefab;
    public Vector2Int[] raftPartsData = new Vector2Int[6];

    //public float genMaxHeight;

    public ProceduralWorld(float minHeight, float maxHeight, int gridSize, float detail, int seed, GenerationType genType, GameObject[] rockPrefabs, float rockprobability)
    {
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
        Vector2 midpoint = new Vector2(heights.GetLength(0) / 2, heights.GetLength(1) / 2);


        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int z = 0; z < heights.GetLength(1); z++)
            {
                float randomHeight = 0;
                float distance = Vector2.Distance(midpoint, new Vector2(x, z));

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
                        randomHeight = Mathf.Cos(distance / detail) * maxHeight;
                        break;
                    default:
                        Debug.LogError("Unexpected enum");
                        break;
                    case GenerationType.island:
                        float pSeed = ProceduralGenerator.instance.GetPerlinSeed();
                        float perlinXisland = (float)x / detail + pSeed;
                        float perlinZisland = (float)z / detail + pSeed;

                        randomHeight = (Mathf.PerlinNoise(perlinXisland, perlinZisland) - minHeight) * maxHeight + (Mathf.Cos(distance / detail) * maxHeight);

                        break;
                }
                
                heights[x, z] = randomHeight;

                float rockRand = UnityEngine.Random.Range(0.0f, 1.0f);

                if(rockRand - (randomHeight / 500) < rockprobability)
                {
                    int t = UnityEngine.Random.Range(0, rockPrefabs.Length);
                    Vector3Int rock = new Vector3Int(x, z, t);
                    rockData.Add(rock);
                }

                if(randomHeight > 60)
                {
                    if(portalData == Vector2.zero)
                    {
                        //Debug.Log(randomHeight);
                        portalData = new Vector2Int(x, z);
                    }
                }
                
            }
        }

        for (int i = 0; i < raftPartsData.Length; i++)
        {
            int x = UnityEngine.Random.Range(heights.GetLength(0) / 5, heights.GetLength(0) - (heights.GetLength(0) / 5));
            int y = UnityEngine.Random.Range(heights.GetLength(1) / 5, heights.GetLength(1) - (heights.GetLength(1) / 5));
            raftPartsData[i] = new Vector2Int(x, y);
        }

        //for (int i = 0; i < heights.GetLength(0); i++)
        //{
        //    for (int x = 0; x < heights.GetLength(1); x++)
        //    {
        //        if (genMaxHeight < heights[i, x])
        //            genMaxHeight = heights[i,x];
        //    }
        //}
    }
}

public enum GenerationType
{
    Random,
    Perlin,
    cosine,
    island
}