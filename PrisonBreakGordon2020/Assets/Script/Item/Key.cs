using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Key : AccessItemHover, IInteractable
{ 
    // accessItem properties
    AccessItem AccessItem;

    [Header("accessItem properties")]
    public int keyId;
    public string keyName;
    public float weight;

    protected override void Start()
    {
        base.Start();
        AccessItem = new AccessItem(keyName, weight, keyId);
    }

    private void Update()
    {
        HoverTransform();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Action();
        }
    }

    public void Action()
    {
        Inventory.instance.AddItem(this.AccessItem);
        InventoryUI.instance.AddItemSlot(this.AccessItem, inventoryicon);

        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        // here comes dropping the item
    }
}
