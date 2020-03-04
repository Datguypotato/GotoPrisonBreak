using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScape : LandScape
{


    private Terrain t;

    private void Start()
    {
        t = FindObjectOfType<Terrain>();

        if(t == null)
        {
            Debug.LogError("There is no terrain");
        }
        else
        {
            Generate();
        }
    }

    public override void Generate()
    {
        Terrain.activeTerrain.terrainData.heightmapResolution = ProceduralGenerator.instance.world.gridSize;
        Terrain.activeTerrain.terrainData.SetHeights(0, 0, ProceduralGenerator.instance.world.heights);
        StartCoroutine(Wavy());
    }

    IEnumerator Wavy()
    {
        for (int i = 0; i < pre; i++)
        {

        }

        ProceduralGenerator.instance.Test();
        Terrain.activeTerrain.terrainData.SetHeights(0, 0, ProceduralGenerator.instance.world.heights);

        yield return new WaitForEndOfFrame();
        StartCoroutine(Wavy());
    }
}
