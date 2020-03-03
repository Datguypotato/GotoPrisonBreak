using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScape : MonoBehaviour
{
    public GameObject prefab; //= GameObject.CreatePrimitive(PrimitiveType.Cube);

    public float maxHeight = 1f;
    public float minHeight = 0f;

    public int gridSize = 5;
    [Tooltip("The lower the number the more difference in height")]
    public float detail = 5.0f;

    private void Start()
    {
        ProceduralGenerator.instance.SetSeet(10);
        Generate();

        
    }

    public void Generate()
    {
        if(ProceduralGenerator.instance != null)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    //float r = Random.Range(minHeight, maxHeight);
                    float perlinX = x / detail + ProceduralGenerator.instance.GetPerlinSeed();
                    float perlinZ = z / detail + ProceduralGenerator.instance.GetPerlinSeed();
                    float r = (Mathf.PerlinNoise(perlinX, perlinZ) -minHeight) * maxHeight;


                    Vector3 randomPos = new Vector3(x, r, z) + transform.position;
                    GameObject temp = Instantiate(prefab, randomPos, Quaternion.identity, ProceduralGenerator.instance.transform);

                    // color
                    float height = 1 - temp.transform.position.y / maxHeight;
                    temp.GetComponent<MeshRenderer>().material.color = new Color(height, height, height);
                }
            }

        }
        else
        {
            Debug.LogError("There is no ProceduralGenerator class in this scene");
        }
        
    }
}
