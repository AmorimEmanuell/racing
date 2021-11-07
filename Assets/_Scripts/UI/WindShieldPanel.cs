#pragma warning disable 0649
using UnityEngine;

public class WindShieldPanel : MonoBehaviour
{
    [SerializeField] private CheckpointPanel _checkpointPanel;
    [SerializeField] private MainMenu _mainMenu;

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.CheckpointReached, OnCheckpointReached);
        EventBus.Register(EventBus.EventType.GamePaused, OnGamePaused);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.CheckpointReached, OnCheckpointReached);
        EventBus.Unregister(EventBus.EventType.GamePaused, OnGamePaused);
    }

    private void OnCheckpointReached()
    {
        _checkpointPanel.Show(GameData.ElapsedTime.Get().ToString("F2"));

        CancelInvoke(nameof(HideCheckpointPanel));
        Invoke(nameof(HideCheckpointPanel), 2f);
    }

    private void HideCheckpointPanel()
    {
        _checkpointPanel.Hide();
    }

    private void OnGamePaused()
    {
        _checkpointPanel.Hide();
        _mainMenu.Show();
    }
}
