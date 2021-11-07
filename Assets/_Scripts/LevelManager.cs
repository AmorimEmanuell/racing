#pragma warning disable 0649
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CheckpointSystem _checkpointSystem;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float[] _timeGoals;

    private int _currentTimeGoal;
    private Objective _objective;

    private void Start()
    {
        _objective = new Objective(_timeGoals[_currentTimeGoal], _currentTimeGoal);
        GameData.CurrentObjective.Set(_objective);
    }

    private void OnEnable()
    {
        _checkpointSystem.OnFinalCheckpointReached += OnFinalCheckpointReached;

        EventBus.Register(EventBus.EventType.UnpauseGame, OnGameUnpaused);
        EventBus.Register(EventBus.EventType.RestartGame, OnGameRestarted);
    }

    private void OnDisable()
    {
        _checkpointSystem.OnFinalCheckpointReached -= OnFinalCheckpointReached;

        EventBus.Unregister(EventBus.EventType.UnpauseGame, OnGameUnpaused);
        EventBus.Unregister(EventBus.EventType.RestartGame, OnGameRestarted);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBus.Trigger(EventBus.EventType.PauseGame);
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

    private void OnFinalCheckpointReached()
    {
        Time.timeScale = 0f;
    }

    private void OnGameRestarted(object obj)
    {
        GameData.ElapsedTime.Set(0);

        _currentTimeGoal = 0;
        _objective.Update(_timeGoals[_currentTimeGoal], _currentTimeGoal);
        GameData.CurrentObjective.Set(_objective);

        EventBus.Trigger(EventBus.EventType.ResetCar, _startPosition);

        Time.timeScale = 1f;
    }

    private void OnGameUnpaused(object obj)
    {
        Time.timeScale = 1;
    }
}
