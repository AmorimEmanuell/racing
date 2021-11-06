#pragma warning disable 0649
using UnityEngine;

public class WindShieldPanel : MonoBehaviour
{
    [SerializeField] private CheckpointPanel _checkpointPanel;

    private void Awake()
    {
        EventBus.Register(EventBus.EventType.CheckpointReached, OnCheckpointReached);
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
}
