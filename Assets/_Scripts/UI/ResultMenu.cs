#pragma warning disable 0649
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultMenu : MonoBehaviour
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private TextMeshProUGUI _finalTime;

    private void OnEnable()
    {
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
    }

    private void OnDisable()
    {
        _tryAgainButton.onClick.RemoveAllListeners();
    }

    private void OnTryAgainButtonClicked()
    {
        gameObject.SetActive(false);
        EventBus.Trigger(EventBus.EventType.RestartGame);
    }

    public void DisplayResults()
    {
        gameObject.SetActive(true);
        _finalTime.text = GameData.ElapsedTime.Get().ToString("F2");
    }
}
