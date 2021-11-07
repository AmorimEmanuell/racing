using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus.Trigger(EventBus.EventType.PauseGame);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            EventBus.Trigger(EventBus.EventType.EngageCarBoost);
        }
    }
}
