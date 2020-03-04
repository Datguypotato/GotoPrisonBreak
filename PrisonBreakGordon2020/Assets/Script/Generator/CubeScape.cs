using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScape : LandScape
{
    public GameObject prefab; //= GameObject.CreatePrimitive(PrimitiveType.Cube);
    GameObject[,] gridCubes;


    //water properties
    public float colorTreshold = 0.1f;
    public float colorAmplifier = 1.5f;

    private void Start()
    {
        //ProceduralGenerator.instance.SetSeet(10);
        Generate();
    }

    public override void Generate()
    {
        Debug.Log("Create world");
        Clean();
        if (ProceduralGenerator.instance != null)
        {
            ProceduralWorld w = ProceduralGenerator.instance.world;
            gridCubes = new GameObject[w.heights.GetLength(0), w.heights.GetLength(1)];

            for (int x = 0; x < w.heights.GetLength(0); x++)
            {
                for (int z = 0; z < w.heights.GetLength(1); z++)
                {
                    //int y = ;

                    Vector3 pos = transform.position + new Vector3(x, ProceduralGenerator.instance.world.heights[x, z], z);
                    GameObject temp = Instantiate(prefab, pos, Quaternion.identity, transform);

                    // color
                    float height = 1 - temp.transform.position.y / ProceduralGenerator.instance.world.maxHeight;
                    temp.GetComponent<MeshRenderer>().material.color = new Color(0, 0, height * 1.5f);
                    gridCubes[x, z] = temp;
                }
            }
            //StartCoroutine(WaveWorld());
        }
        else
        {
            Debug.LogError("There is no ProceduralGenerator class in this scene");
        }
    }

    public override void Clean()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    IEnumerator WaveWorld()
    {

        List<GameObject> temps = new List<GameObject>();

        for (int x = 0; x < 35; x++)
        {
            for (int z = 0; z < 35; z++)
            {
                Vector3 pos = transform.position + new Vector3(x, Mathf.Cos(x + z + Time.time), z);
                gridCubes[x, z].transform.position = pos;


                // color
                float height = gridCubes[x, z].transform.position.y / 1;
                if(height > colorTreshold)
                {
                    gridCubes[x, z].GetComponent<MeshRenderer>().material.color = new Color(0, 0, height * colorAmplifier);
                }
            }
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(WaveWorld());
    }
}