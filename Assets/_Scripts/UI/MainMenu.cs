#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDestroy()
    {
        _resumeButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
    }

    private void OnResumeButtonClicked()
    {
        gameObject.SetActive(false);
        EventBus.Trigger(EventBus.EventType.UnpauseGame);
    }

    private void OnRestartButtonClicked()
    {
        gameObject.SetActive(false);
        EventBus.Trigger(EventBus.EventType.UnpauseGame);
        EventBus.Trigger(EventBus.EventType.RestartGame);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
