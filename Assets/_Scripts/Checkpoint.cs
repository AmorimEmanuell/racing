using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Action OnPlayerReach;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerReach?.Invoke();
        }
    }
}
