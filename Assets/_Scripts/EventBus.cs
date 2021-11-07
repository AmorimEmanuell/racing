using System;
using System.Collections.Generic;

public static class EventBus
{
    public enum EventType
    {
        CheckpointReached,
        PauseGame,
        UnpauseGame,
        RestartGame,
        ResetCar,
        DisplayResult,
        EngageCarBoost
    }

    private static Dictionary<EventType, List<Action<object>>> _registeredEvents = new Dictionary<EventType, List<Action<object>>>();

    public static void Register(EventType eventType, Action<object> action)
    {
        if (_registeredEvents.TryGetValue(eventType, out List<Action<object>> registeredActions))
        {
            registeredActions.Add(action);
            return;
        }

        _registeredEvents.Add(eventType, new List<Action<object>> { action });
    }

    public static void Unregister(EventType eventType, Action<object> action)
    {
        if (_registeredEvents.TryGetValue(eventType, out List<Action<object>> registeredActions))
        {
            registeredActions.Remove(action);
        }
    }

    public static void Trigger(EventType eventType, object args = null)
    {
        if (_registeredEvents.TryGetValue(eventType, out List<Action<object>> registeredActions))
        {
            for (var i=0; i<registeredActions.Count; i++)
            {
                registeredActions[i].Invoke(args);
            }
        }
    }
}
