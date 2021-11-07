#pragma warning disable 0649
using System;
using TMPro;
using UnityEngine;

public class CarPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _elapsedTime;
    [SerializeField] private TextMeshProUGUI _currentObjective;
    [SerializeField] private StarPanel _starPanel;

    private void OnEnable()
    {
        GameData.ElapsedTime.OnChange += UpdateElapsedTime;
        GameData.CurrentObjective.OnChange += OnObjectiveChanged;
    }

    private void OnDisable()
    {
        GameData.ElapsedTime.OnChange -= UpdateElapsedTime;
        GameData.CurrentObjective.OnChange -= OnObjectiveChanged;
    }

    private void UpdateElapsedTime(float elapsedTime)
    {
        _elapsedTime.text = elapsedTime.ToString("F2");
    }

    private void OnObjectiveChanged(Objective objective)
    {
        _currentObjective.text = objective.TimeGoal.ToString();
        _starPanel.ShowStars(objective.Current);
    }
}
