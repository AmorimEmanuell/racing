using UnityEngine;

public class WallOfDoom : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventBus.Trigger(EventBus.EventType.RestartGame);
        }
    }
}
