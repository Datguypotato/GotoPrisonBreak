﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProceduralGenerator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ProceduralGenerator gen = (ProceduralGenerator)target;
        if(GUILayout.Button("Generate World"))
        {
            ProceduralWorld w = ProceduralGenerator.instance.world;
            ProceduralGenerator.instance.world = new ProceduralWorld(w.minHeight, w.maxHeight, w.gridSize, w.detail, w.seed, w.genType);
            ProceduralGenerator.instance.SetSeed(w.seed);

            LandScape scape = FindObjectOfType<LandScape>();
            scape.Generate();
            for (int i = 0; i < 2; i++)
            {

            }
        }
    }
}
