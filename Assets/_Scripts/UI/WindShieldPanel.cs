#pragma warning disable 0649
using UnityEngine;

public class WindShieldPanel : MonoBehaviour
{
    [SerializeField] private CheckpointPanel _checkpointPanel;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private ResultMenu _resultMenu;

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.CheckpointReached, OnCheckpointReached);
        EventBus.Register(EventBus.EventType.PauseGame, OnGamePaused);
        EventBus.Register(EventBus.EventType.DisplayResult, OnDisplayResult);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.CheckpointReached, OnCheckpointReached);
        EventBus.Unregister(EventBus.EventType.PauseGame, OnGamePaused);
        EventBus.Unregister(EventBus.EventType.DisplayResult, OnDisplayResult);
    }

    private void OnCheckpointReached(object obj)
    {
        _checkpointPanel.Show(GameData.ElapsedTime.Get().ToString("F2"));

        CancelInvoke(nameof(HideCheckpointPanel));
        Invoke(nameof(HideCheckpointPanel), 2f);
    }

    private void HideCheckpointPanel()
    {
        _checkpointPanel.Hide();
    }

    private void OnGamePaused(object obj)
    {
        _checkpointPanel.Hide();
        _mainMenu.Show();
    }

    private void OnDisplayResult(object obj)
    {
        CancelInvoke(nameof(HideCheckpointPanel));
        HideCheckpointPanel();

        _resultMenu.DisplayResults();
    }
}
