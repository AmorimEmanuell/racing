#pragma warning disable 0649
using TMPro;
using UnityEngine;

public class CheckpointPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _checkpointTime;

    public void Show(float elapsedTime)
    {
        _checkpointTime.SetText("{0:2}", elapsedTime);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
