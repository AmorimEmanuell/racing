﻿#pragma warning disable 0649
using System;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] private Checkpoint[] _checkpoints;

    private int _currentCheckpoint;

    public Action OnFinalCheckpointReached;

    private void Start()
    {
        EnableCurrentCheckpoint(true);
    }

    private void EnableCurrentCheckpoint(bool enabled)
    {
        var currentCheckpoint = _checkpoints[_currentCheckpoint];
        currentCheckpoint.gameObject.SetActive(enabled);

        if (enabled)
        {
            currentCheckpoint.OnPlayerReached += OnPlayerReachedCheckpoint;
        }
        else
        {
            currentCheckpoint.OnPlayerReached -= OnPlayerReachedCheckpoint;
        }
    }

    private void OnPlayerReachedCheckpoint()
    {
        EnableCurrentCheckpoint(false);

        if (++_currentCheckpoint == _checkpoints.Length)
        {
            OnFinalCheckpointReached?.Invoke();
            return;
        }

        EventBus.Trigger(EventBus.EventType.CheckpointReached);
        EnableCurrentCheckpoint(true);
    }
}