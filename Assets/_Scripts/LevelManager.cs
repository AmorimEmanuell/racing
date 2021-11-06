#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> _checkpoints;

    private int _currentCheckpoint;

    private void Awake()
    {
        ActivateNextCheckpoint();
    }

    private void Update()
    {
        GameData.ElapsedTime.Set(GameData.ElapsedTime.Get() + Time.deltaTime);
    }

    private void ActivateNextCheckpoint()
    {
        var currentCheckpoint = _checkpoints[_currentCheckpoint];
        currentCheckpoint.OnPlayerReach += OnCheckpointReached;
        currentCheckpoint.gameObject.SetActive(true);
    }

    private void DisableCurrentCheckpoint()
    {
        var currentCheckpoint = _checkpoints[_currentCheckpoint];
        currentCheckpoint.OnPlayerReach -= OnCheckpointReached;
        currentCheckpoint.gameObject.SetActive(false);
    }

    private void OnCheckpointReached()
    {
        DisableCurrentCheckpoint();

        if (++_currentCheckpoint == _checkpoints.Count)
        {
            Debug.Log("Game Finished");
            Time.timeScale = 0f;
            // TODO: Finish Game
            return;
        }

        EventBus.Trigger(EventBus.EventType.CheckpointReached);

        ActivateNextCheckpoint();
    }
}
