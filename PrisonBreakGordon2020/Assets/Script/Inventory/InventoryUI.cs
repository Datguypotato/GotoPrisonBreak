using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public List<InventoryUISlot> slots;
    public Sprite emptySlot;

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

    public void AddItemSlot(Item item, Sprite icon)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].filled)
            {
                slots[i].FillSlot(icon, item);
                break;
            }
        }
    }

    public void RemoveItemSlot(int slotIndex)
    {
        slots[slotIndex].EmptySlot();
    }
}
