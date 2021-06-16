using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private Transform grid;

    private readonly List<InventoryItem> currentItems = new List<InventoryItem>();

    private bool isOpen;

    private void Awake()
    {
        EventManager.Register(EventManager.EventType.InventoryToggle, Toggle);
    }

    private void Toggle(object obj)
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open(obj);
        }
    }

    private void Open(object obj)
    {
        if (obj is List<ItemData> objectsPicked)
        {
            for (var i = 0; i < objectsPicked.Count; i++)
            {
                var item = Instantiate(itemPrefab, grid);
                item.SetSprite(objectsPicked[i].InventorySprite);
                currentItems.Add(item);
            }

            grid.gameObject.SetActive(true);
            isOpen = true;
        }
    }

    private void Close()
    {
        foreach (var item in currentItems)
        {
            Destroy(item.gameObject);
        }

        currentItems.Clear();

        grid.gameObject.SetActive(false);
        isOpen = false;
    }
}
