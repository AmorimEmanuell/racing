#pragma warning disable 0649
using System;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] private Checkpoint[] _checkpoints;

    [Space(10)]
    [SerializeField] private Transform _smokeFX;

    private int _currentCheckpoint;

    public Action OnFinalCheckpointReached;

    private void Start()
    {
        EnableCurrentCheckpoint(true);
    }

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.RestartGame, OnGameRestarted);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.RestartGame, OnGameRestarted);
    }

    private void OnGameRestarted(object obj)
    {
        if (_currentCheckpoint < _checkpoints.Length)
        {
            EnableCurrentCheckpoint(false);
        }

        _currentCheckpoint = 0;
        EnableCurrentCheckpoint(true);
    }

    private void EnableCurrentCheckpoint(bool enabled)
    {
        var currentCheckpoint = _checkpoints[_currentCheckpoint];
        currentCheckpoint.gameObject.SetActive(enabled);

        if (enabled)
        {
            currentCheckpoint.OnPlayerReached += OnPlayerReachedCheckpoint;

            _smokeFX.SetParent(currentCheckpoint.transform);
            _smokeFX.localPosition = Vector3.zero;
            _smokeFX.gameObject.SetActive(true);
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
