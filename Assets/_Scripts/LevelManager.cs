#pragma warning disable 0649
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Checkpoint[] _checkpoints;
    [SerializeField] private float[] _timeGoals;

    private int _currentCheckpoint;
    private int _currentTimeGoal;

    private void Start()
    {
        ActivateNextCheckpoint();
        GameData.CurrentObjective.Set(new Objective(_timeGoals[_currentTimeGoal], _currentTimeGoal));
    }

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.GameUnpaused, OnGameUnpaused);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.GameUnpaused, OnGameUnpaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus.Trigger(EventBus.EventType.GamePaused);
            Time.timeScale = 0;
            return;
        }

        GameData.ElapsedTime.Set(GameData.ElapsedTime.Get() + Time.deltaTime);

        if (_currentTimeGoal == _timeGoals.Length)
        {
            return;
        }

        var elapsedTime = GameData.ElapsedTime.Get();
        if (elapsedTime <= _timeGoals[_currentTimeGoal])
        {
            return;
        }

        if (++_currentTimeGoal == _timeGoals.Length)
        {
            GameData.CurrentObjective.Set(new Objective(0, _currentTimeGoal));
            return;
        }

        GameData.CurrentObjective.Set(new Objective(_timeGoals[_currentTimeGoal], _currentTimeGoal));
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

        if (++_currentCheckpoint == _checkpoints.Length)
        {
            Debug.Log("Game Finished");
            Time.timeScale = 0f;
            // TODO: Finish Game
            return;
        }

        EventBus.Trigger(EventBus.EventType.CheckpointReached);

        ActivateNextCheckpoint();
    }

    private void OnGameUnpaused()
    {
        Time.timeScale = 1;
    }
}
