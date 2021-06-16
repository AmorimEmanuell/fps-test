using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image image;

    private ItemData itemData;

    public Action<ItemData> OnSelected;

    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        image.sprite = itemData.InventorySprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelected?.Invoke(itemData);
    }
}
