using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftPart : AccessItemHover, IInteractable
{
    Item raftItem;

    protected override void Start()
    {
        base.Start();

        raftItem = new Item("raft part", 1);
    }

    private void Update()
    {
        HoverTransform();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Action();
    }

    public void Action()
    {
        Inventory.instance.AddItem(raftItem);
        InventoryUI.instance.AddItemSlot(this.raftItem, inventoryicon);

        gameObject.SetActive(false);
    }
}
