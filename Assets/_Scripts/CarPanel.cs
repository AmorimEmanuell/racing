#pragma warning disable 0649
using TMPro;
using UnityEngine;

public class CarPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _elapsedTime;

    private void OnEnable()
    {
        GameData.ElapsedTime.OnChange += UpdateElapsedTime;
    }

    private void OnDisable()
    {
        GameData.ElapsedTime.OnChange -= UpdateElapsedTime;
    }

    private void UpdateElapsedTime(float elapsedTime)
    {
        _elapsedTime.text = elapsedTime.ToString("F2");
    }
}
