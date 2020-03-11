using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScape : LandScape
{
    public Terrain t;
    public TerrainData tData;

    private void Start()
    {
        t = FindObjectOfType<Terrain>();

        if(t == null)
        {
            Debug.LogError("There is no terrain");
        }
        else
        {
            Clean();
            Generate();
        }
    }

    public override void Generate()
    {
        ProceduralWorld w = ProceduralGenerator.instance.world;

        tData.heightmapResolution = w.gridSize;

        float[,] dividedHeights = new float[w.heights.GetLength(0), w.heights.GetLength(1)];
        for (int x = 0; x < w.heights.GetLength(0); x++)
        {
            for (int z = 0; z < w.heights.GetLength(1); z++)
            {
                dividedHeights[x, z] = w.heights[x, z] / 1000;
            }
        }

        tData.SetHeights(0, 0, dividedHeights);
        
        for (int i = 0; i < ProceduralGenerator.instance.world.rockData.Count; i++)
        {

            Vector3 rockpos = new Vector3(
                Map(w.rockData[i].x, 0, w.gridSize, t.GetPosition().x, t.GetPosition().x + tData.size.x),
                0,
                Map(w.rockData[i].y, 0, w.gridSize, t.GetPosition().z, t.GetPosition().z + tData.size.z));

            rockpos.y = t.SampleHeight(rockpos);

            Instantiate(w.rockPrefabs[0], rockpos, Quaternion.identity, transform);
        }
    }

    public override void Clean()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    //does not work 
    //too heavy for computers
    //    IEnumerator GenerateWave()
    //    {
    //        ProceduralWorld w = ProceduralGenerator.instance.world;
    //        float[,] heights = new float[w.heights.GetLength(0), w.heights.GetLength(1)];


    //        for (int x = 0; x < 64; x++)
    //        {
    //            for (int z = 0; z < 64; z++)
    //            {
    //                float randomHeight = Mathf.Cos(x + z + Time.time) / w.detail * w.maxHeight;

    //                heights[x, z] = randomHeight;
    //                tData.SetHeightsDelayLOD(0, 0, heights);
    //            }
    //        }
    //        yield return new WaitForSeconds(1);
    //        StartCoroutine(GenerateWave());
    //    }


    public float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
