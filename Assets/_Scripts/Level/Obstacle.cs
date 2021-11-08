using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private static CarController _carController;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (_carController == null)
        {
            _carController = other.GetComponent<CarController>();
        }

        _carController.ObstacleHit();
    }
}
