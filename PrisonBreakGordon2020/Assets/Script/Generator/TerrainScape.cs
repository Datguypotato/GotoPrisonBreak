using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainScape : LandScape
{
    public Terrain t;
    public TerrainData tData;

    public Transform PortalExitTrans;
    public GameObject raftPrefab;

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
        float[] splatWeights = new float[tData.alphamapLayers];

        // the last array index should be the number of layers the terrain has
        //float[,,] splatmapData = new float[w.heights.GetLength(0), w.heights.GetLength(1), tData.alphamapLayers];

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
                    //Debug.Log(w.heights[x, z]);
                }

                #region terrain painting
                //// painting terrain
                //float normalizeX = (float)x / (float)w.heights.GetLength(0);
                //float normalizeY = (float)z / (float)w.heights.GetLength(1);

                //float height = tData.GetHeight(Mathf.RoundToInt(normalizeX * tData.heightmapHeight), Mathf.RoundToInt(normalizeY * tData.heightmapWidth));

                //Vector3 normal = tData.GetInterpolatedNormal(normalizeX, normalizeX);


                //splatWeights[0] = 1;
                //splatWeights[1] = 0;
                //splatWeights[2] = 0;
                //splatWeights[3] = 0;

                //float splatWeightSum = splatWeights.Sum();

                //// assign these texture
                //for (int i = 0; i < tData.alphamapLayers; i++)
                //{
                //    splatWeights[i] /= splatWeightSum;

                //    splatmapData[x, z, i] = splatWeights[i];
                //}
                #endregion
            }
        }

        tData.SetHeights(0, 0, dividedHeights);
        //tData.SetAlphamaps(0, 0, splatmapData);


        // spawning rocks
        for (int i = 0; i < w.rockData.Count; i++)
        {

            Vector3 rockpos = new Vector3(
                Map(w.rockData[i].x, 0, w.gridSize, t.GetPosition().x, t.GetPosition().x + tData.size.x),
                0,
                Map(w.rockData[i].y, 0, w.gridSize, t.GetPosition().z, t.GetPosition().z + tData.size.z));

            rockpos.y = t.SampleHeight(rockpos) + transform.position.y;
            Quaternion rockRot = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)); ;

            Instantiate(w.rockPrefabs[Random.Range(0, w.rockPrefabs.Length)], rockpos, rockRot, transform);
            
        }

        // setting portal
        
        Vector3 portalPos = new Vector3(
            Map(w.portalData.x, 0, w.gridSize, t.GetPosition().x, t.GetPosition().x + tData.size.x),
            0,
            Map(w.portalData.y, 0, w.gridSize, t.GetPosition().z, t.GetPosition().z + tData.size.z));
        portalPos.y = t.SampleHeight(portalPos) + transform.position.y;

        PortalExitTrans.position = portalPos;

        // setting raftparts
        for (int i = 0; i < w.raftPartsData.Length; i++)
        {
            Vector3 raftPos = new Vector3(
                Map(w.raftPartsData[i].x, 0, w.gridSize, t.GetPosition().x, t.GetPosition().x + tData.size.x),
                0,
                Map(w.raftPartsData[i].y, 0, w.gridSize, t.GetPosition().z, t.GetPosition().z + tData.size.z));

            raftPos.y = t.SampleHeight(raftPos) + transform.position.y;

            Instantiate(raftPrefab, raftPos, Quaternion.identity);
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
