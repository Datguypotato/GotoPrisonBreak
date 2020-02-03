using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Vector2 xBoundaries;
    Vector2 zBoundaries;


    private void Start()
    {
        // calculating the bounda
        Collider box = GetComponent<BoxCollider>();
        xBoundaries = new Vector2(box.bounds.min.x, box.bounds.max.x);
        zBoundaries = new Vector2(box.bounds.min.z, box.bounds.max.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerFollow>().SetupNewLevel(this.transform, xBoundaries, zBoundaries);
        }
    }
}
