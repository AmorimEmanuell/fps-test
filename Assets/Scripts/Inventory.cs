using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> picked = new List<ItemData>();

    public const string OpenEvent = "OpenInventory";

    private void Awake()
    {
        EventManager.Register(Collectable.PickupEvent, PickupCollectableHandler);
    }

    private void PickupCollectableHandler(object obj)
    {
        if (obj is ItemData itemData)
        {
            picked.Add(itemData);
        }
    }
}
