using cakeslice;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Outline outline;

    public ItemData ItemData => itemData;

    private void Start()
    {
        outline.enabled = false;
    }

    public void OnPointerEnter()
    {
        outline.enabled = true;
    }

    public void OnPointerExit()
    {
        outline.enabled = false;
    }

    public void OnPointerClick()
    {
        EventManager.Trigger(EventManager.EventType.CollectablePickup, this);
        gameObject.SetActive(false);
    }
}
