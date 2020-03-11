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

        // the last array index should be the number of layers the terrain has
        float[,,] map = new float[w.heights.GetLength(0), w.heights.GetLength(1), 4];

        float[,] dividedHeights = new float[w.heights.GetLength(0), w.heights.GetLength(1)];
        for (int x = 0; x < w.heights.GetLength(0); x++)
        {
            for (int z = 0; z < w.heights.GetLength(1); z++)
            {
                if(x == 0 || x == w.heights.GetLength(0) || z == 0 || z == w.heights.GetLength(1))
                {
                    dividedHeights[x, z] = 0;
                }
                else
                {
                    dividedHeights[x, z] = w.heights[x, z] / 1000;
                }

                // painting terrain
                
                // divide max height to 3 values and those three value is going to be the limit for each layer


                // assign these texture
                //tData.SetAlphamaps(x,z, )
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
            Quaternion rockRot = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)); ;

            Instantiate(w.rockPrefabs[Random.Range(0, w.rockPrefabs.Length)], rockpos, rockRot, transform);
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
