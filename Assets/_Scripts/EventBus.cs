using System;
using System.Collections.Generic;

public static class EventBus
{
    public enum EventType
    {
        CheckpointReached,
        GamePaused,
        GameUnpaused,
        GameRestarted,
        GameFinished
    }

    private static Dictionary<EventType, List<Action>> _registeredEvents = new Dictionary<EventType, List<Action>>();

    public static void Register(EventType eventType, Action action)
    {
        if (_registeredEvents.TryGetValue(eventType, out List<Action> registeredActions))
        {
            registeredActions.Add(action);
            return;
        }

        _registeredEvents.Add(eventType, new List<Action> { action });
    }

    public static void Unregister(EventType eventType, Action action)
    {
        if (_registeredEvents.TryGetValue(eventType, out List<Action> registeredActions))
        {
            registeredActions.Remove(action);
        }
    }

    public static void Trigger(EventType eventType)
    {
        if (_registeredEvents.TryGetValue(eventType, out List<Action> registeredActions))
        {
            for (var i=0; i<registeredActions.Count; i++)
            {
                registeredActions[i].Invoke();
            }
        }
    }
}
