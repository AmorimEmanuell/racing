#pragma warning disable 0649
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CheckpointSystem _checkpointSystem;
    [SerializeField] private float[] _timeGoals;

    private int _currentTimeGoal;

    private void Start()
    {
        GameData.CurrentObjective.Set(new Objective(_timeGoals[_currentTimeGoal], _currentTimeGoal));
    }

    private void OnEnable()
    {
        _checkpointSystem.OnFinalCheckpointReached += OnFinalCheckpointReached;
        EventBus.Register(EventBus.EventType.GameUnpaused, OnGameUnpaused);
    }

    private void OnDisable()
    {
        _checkpointSystem.OnFinalCheckpointReached -= OnFinalCheckpointReached;
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

    private void OnFinalCheckpointReached()
    {
        Time.timeScale = 0;
    }

    private void OnGameUnpaused()
    {
        Time.timeScale = 1;
    }
}
