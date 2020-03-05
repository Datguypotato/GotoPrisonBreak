using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    public static ProceduralGenerator instance;

    private int seed;
    private float perlinSeed;

    public ProceduralWorld world;

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

        world = new ProceduralWorld(world.minHeight, world.maxHeight, world.gridSize, world.detail, world.seed, world.genType);
        SetSeed(world.seed);
    }

    private void Start()
    {

    }

    public void SetSeed(int newSeed)
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
