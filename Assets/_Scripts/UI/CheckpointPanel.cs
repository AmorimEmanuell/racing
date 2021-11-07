#pragma warning disable 0649
using TMPro;
using UnityEngine;

public class CheckpointPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _checkpointTime;

    public void Show(string time)
    {
        _checkpointTime.text = time;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
