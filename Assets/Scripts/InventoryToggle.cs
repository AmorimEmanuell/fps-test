using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> componentsToControl = new List<MonoBehaviour>();

    private void Awake()
    {
        EventManager.Register(EventManager.EventType.InventoryToggleConfim, ActivateGameobj);
    }

    private void ActivateGameobj(object obj)
    {
        if (obj is bool isInventoryOpen)
        {
            foreach (var component in componentsToControl)
            {
                component.enabled = !isInventoryOpen;
            }
        }
    }
}
