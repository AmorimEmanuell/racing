using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Action OnPlayerReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerReached?.Invoke();
        }
    }
}
