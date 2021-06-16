using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
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
        Debug.Log("OnPinterClick " + name);
    }
}
