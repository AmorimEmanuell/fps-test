using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private readonly Dictionary<int, Collectable> availableCollectables = new Dictionary<int, Collectable>();

    private void Awake()
    {
        EventManager.Register(EventManager.EventType.CollectablePickup, CollectablePickupHandler);
        EventManager.Register(EventManager.EventType.InventoryItemSelected, InventoryItemSelectedHandler);
    }

    private void CollectablePickupHandler(object obj)
    {
        if (obj is Collectable collectable)
        {
            if (!availableCollectables.ContainsKey(collectable.ItemData.Id))
            {
                collectable.Collider.enabled = false;
                availableCollectables.Add(collectable.ItemData.Id, collectable);
            }
        }
    }

    private void InventoryItemSelectedHandler(object obj)
    {
        if (obj is ItemData itemData)
        {
            if (availableCollectables.TryGetValue(itemData.Id, out Collectable collectable))
            {
                availableCollectables.Remove(itemData.Id);

                collectable.gameObject.SetActive(true);
                EventManager.Trigger(EventManager.EventType.PlaceModeActivate, collectable);
            }
        }
    }
}
