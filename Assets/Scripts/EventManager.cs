using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<EventType, List<Action<object>>> events = new Dictionary<EventType, List<Action<object>>>();

    public enum EventType
    {
        InventoryToggle,
        CollectablePickup
    }

    public static void Register(EventType eventName, Action<object> listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].Add(listener);
        }
        else
        {
            events.Add(eventName, new List<Action<object>>() { listener });
        }
    }

    public static void Unregister(EventType eventName, Action<object> listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].Remove(listener);

            if (events[eventName].Count == 0)
            {
                events.Remove(eventName);
            }
        }
    }

    public static void Trigger(EventType eventName, object param)
    {
        if (events.ContainsKey(eventName))
        {
            foreach(var listener in events[eventName])
            {
                listener.Invoke(param);
            }
        }
    }
}
