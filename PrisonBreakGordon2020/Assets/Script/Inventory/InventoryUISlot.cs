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

    private void Start()
    {
        icon = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void FillSlot(Sprite icon, string name)
    {
        text.text = name;
        this.icon.sprite = icon;
        filled = true;
    }

    public void EmptySlot()
    {
        icon = null;
        text.text = "";
        filled = false;
    }

}
