using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    public const string PickupEvent = "Pickup";

    public void OnPointerEnter()
    {
        Debug.Log("OnPointerEnter " + name);
    }

    public void OnPointerExit()
    {
        Debug.Log("OnPointerExit " + name);
    }

    public void OnPointerClick()
    {
        Debug.Log("OnPointerClick " + name);
        EventManager.Trigger(PickupEvent, this);
        gameObject.SetActive(false);
    }
}
