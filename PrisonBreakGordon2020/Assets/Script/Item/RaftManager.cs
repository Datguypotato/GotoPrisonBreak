using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftManager : MonoBehaviour
{
    public static RaftManager instance;

    public int raftPartCollected = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Inventory.instance.GetAmountLog() == 6)
            {
                // win game
            }
        }
    }
}