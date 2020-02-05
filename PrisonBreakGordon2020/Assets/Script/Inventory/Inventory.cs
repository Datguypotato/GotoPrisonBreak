using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private List<Item> items = new List<Item>(6);

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
        items = new List<Item>();
    }

    private void Start()
    {
        
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

    public bool RemoveItem(int index)
    {
        if(items[index] != null)
        {
            items[index] = null;


            return true;
        }
        else
        {
            return false;
        }
        //// todo change the function so it can be called from InventoryUI
        //if (items.Contains(i))
        //{
        //    items.Remove(i);
        //    currWeight -= i.weight;
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }

    public bool HasKey(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is AccessItem)
            {
                AccessItem it = (AccessItem)items[i];
                if (it.CanOpenDoor(id))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int Count()
    {
        return items.Count;
    }
}
