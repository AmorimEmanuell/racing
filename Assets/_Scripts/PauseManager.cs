using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private void Awake()
    {
        EventBus.Register(EventBus.EventType.UnpauseGame, Unpause);
        EventBus.Register(EventBus.EventType.DisplayResult, Pause);
        EventBus.Register(EventBus.EventType.RestartGame, Unpause);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void OnDestroy()
    {
        EventBus.Unregister(EventBus.EventType.UnpauseGame, Unpause);
        EventBus.Unregister(EventBus.EventType.DisplayResult, Pause);
        EventBus.Unregister(EventBus.EventType.RestartGame, Unpause);
    }

    private void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0f;
            EventBus.Trigger(EventBus.EventType.PauseGame);
        }
    }

    private void Pause(object obj)
    {
        Time.timeScale = 0f;
    }

    private void Unpause(object obj)
    {
        Time.timeScale = 1f;
    }
}
