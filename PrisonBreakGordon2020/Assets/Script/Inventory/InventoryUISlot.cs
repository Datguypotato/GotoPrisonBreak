using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryUISlot : MonoBehaviour
{
    public bool filled = false;

    Image icon;
    TextMeshProUGUI text;
    Item itemInSlot;

    private void Start()
    {
        icon = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FillSlot(Sprite icon, Item item)
    {
        itemInSlot = item;
        text.text = item.name;
        this.icon.sprite = icon;
        filled = true;
    }

    public void EmptySlot()
    {
        if (Inventory.instance.RemoveItem(this.itemInSlot))
        {
            icon.sprite = InventoryUI.instance.emptySlot;
            text.text = "";
            filled = false;
            itemInSlot = null;
        }
    }

}
