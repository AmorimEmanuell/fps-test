using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> picked = new List<ItemData>();

    public const string ToggleEvent = "ToggleInventory";

    private void Awake()
    {
        EventManager.Register(Collectable.PickupEvent, PickupCollectableHandler);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventManager.Trigger(ToggleEvent, picked);
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
