using cakeslice;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline outline;

    public const string PickupEvent = "Pickup";

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
        Debug.Log("OnPointerClick " + name);
        EventManager.Trigger(PickupEvent, this);
        gameObject.SetActive(false);
    }
}
