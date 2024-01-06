using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcher : Singleton<EventDispatcher>
{
    public void RegisterEvent(string eventId, Action<object> callback)
    {
        if (!allCallbacks.ContainsKey(eventId))
        {
            allCallbacks.Add(eventId, null);
        }

        allCallbacks[eventId] += callback;
    }

    public void RemoveEvent(string eventId, Action<object> callback)
    {
        if (allCallbacks.ContainsKey(eventId))
        {
            allCallbacks[eventId] -= callback;
        }
    }

    public void NotifyEvent(string eventId, object param = null)
    {
#if UNITY_EDITOR
        Debug.Log(eventId);
#endif

        if (allCallbacks.ContainsKey(eventId))
        {
            allCallbacks[eventId]?.Invoke(param);
        }
    }

    public void ClearEvent()
    {
        allCallbacks.Clear();
    }

    private Dictionary<string, Action<object>> allCallbacks = new Dictionary<string, Action<object>>();
}