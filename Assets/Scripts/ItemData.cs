using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite inventorySprite;
    [SerializeField] private Collectable collectableObj;

    public int Id => id;
    public Sprite InventorySprite => inventorySprite;
    public Collectable CollectableObj => collectableObj;
}
