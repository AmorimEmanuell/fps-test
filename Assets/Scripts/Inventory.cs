using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> picked = new List<ItemData>();

    private void Awake()
    {
        EventManager.Register(EventManager.EventType.CollectablePickup, PickupCollectableHandler);
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
        if (obj is ItemData itemData)
        {
            picked.Add(itemData);
        }
    }
}
