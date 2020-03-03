using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    public static ProceduralGenerator instance;

    private int seed;
    private float perlinSeed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetSeet(int newSeed)
    {
        seed = newSeed;
        Random.InitState(seed);
        perlinSeed = Random.Range(-10000f, 10000f);
    }

    public float GetPerlinSeed()
    {
        return perlinSeed;
    }
}
