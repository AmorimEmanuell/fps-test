using System;
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
                item.SetItemData(objectsPicked[i]);
                item.OnSelected += OnItemSelected;
                currentItems.Add(item);
            }

            grid.gameObject.SetActive(true);
            isOpen = true;

            EventManager.Trigger(EventManager.EventType.InventoryToggleConfim, true);
        }
    }

    private void Close()
    {
        foreach (var item in currentItems)
        {
            item.OnSelected -= OnItemSelected;
            Destroy(item.gameObject);
        }

        currentItems.Clear();

        grid.gameObject.SetActive(false);
        isOpen = false;

        EventManager.Trigger(EventManager.EventType.InventoryToggleConfim, false);
    }

    private void OnItemSelected(ItemData itemData)
    {
        Close();
        EventManager.Trigger(EventManager.EventType.InventoryItemSelected, itemData);
    }
}
