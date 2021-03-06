#pragma warning disable 0649
using TMPro;
using UnityEngine;

public class ObjectivesPanel : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _elapsedTime;
    [SerializeField] private TextMeshProUGUI _currentObjective;

    private void OnEnable()
    {
        GameData.ElapsedTime.OnChange += UpdateElapsedTime;
        GameData.CurrentObjective.OnChange += OnObjectiveChanged;

        EventBus.Register(EventBus.EventType.RestartGame, OnGameRestarted);
        EventBus.Register(EventBus.EventType.DisplayResult, OnDisplayResult);
    }

    private void OnDisable()
    {
        GameData.ElapsedTime.OnChange -= UpdateElapsedTime;
        GameData.CurrentObjective.OnChange -= OnObjectiveChanged;

        EventBus.Unregister(EventBus.EventType.RestartGame, OnGameRestarted);
        EventBus.Unregister(EventBus.EventType.DisplayResult, OnDisplayResult);
    }

    private void UpdateElapsedTime(float elapsedTime)
    {
        _elapsedTime.SetText("{0:2}", elapsedTime);
    }

    private void OnObjectiveChanged(Objective objective)
    {
        _currentObjective.SetText("{0}", objective.TimeGoal);
    }

    private void OnGameRestarted(object obj)
    {
        _canvas.enabled = true;
    }

    private void OnDisplayResult(object obj)
    {
        _canvas.enabled = false;
    }
}
