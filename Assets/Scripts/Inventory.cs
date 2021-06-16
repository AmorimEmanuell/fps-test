using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Collectable> picked = new List<Collectable>();

    private void Awake()
    {
        EventManager.Register(Collectable.PickupEvent, PickupCollectableHandler);
    }

    private void PickupCollectableHandler(object obj)
    {
        if (obj is Collectable collectable)
        {
            Debug.Log("Inventory::PickupCollectableHandler " + collectable.name);
            picked.Add(collectable);
        }
    }
}
