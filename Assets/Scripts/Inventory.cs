using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // This is serializeField so i can easily debug on inspector
    [SerializeField] private List<ItemData> picked = new List<ItemData>();

    private void Awake()
    {
        EventManager.Register(EventManager.EventType.CollectablePickup, PickupCollectableHandler);
        EventManager.Register(EventManager.EventType.InventoryItemSelected, InventoryItemSelectedHandler);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventManager.Trigger(EventManager.EventType.InventoryToggle, picked);
        }
    }

    private void PickupCollectableHandler(object obj)
    {
        if (obj is Collectable collectable)
        {
            picked.Add(collectable.ItemData);
        }
    }

    private void InventoryItemSelectedHandler(object obj)
    {
        if (obj is ItemData itemData)
        {
            picked.Remove(itemData);
        }
    }
}
