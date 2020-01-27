using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<Item> items;

    public float maxWeight = 100;
    public float currWeight = 0;

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

    private void Start()
    {
        items = new List<Item>();
        currWeight = 0;
    }

    public bool AddItem(Item i)
    {
        if(currWeight + i.weight <= maxWeight)
        {
            items.Add(i);
            currWeight += i.weight;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Count()
    {
        return items.Count;
    }
}
