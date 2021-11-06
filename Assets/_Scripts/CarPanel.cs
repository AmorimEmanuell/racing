#pragma warning disable 0649
using TMPro;
using UnityEngine;

public class CarPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _velocity;
    [SerializeField] private TextMeshProUGUI _elapsedTime;

    private void OnEnable()
    {
        CarData.Velocity.OnChange += UpdateVelocity;
        GameData.ElapsedTime.OnChange += UpdateElapsedTime;
    }

    private void OnDisable()
    {
        CarData.Velocity.OnChange -= UpdateVelocity;
        GameData.ElapsedTime.OnChange -= UpdateElapsedTime;
    }

    private void UpdateVelocity(float velocity)
    {
        _velocity.text = velocity.ToString("F2");
    }

    private void UpdateElapsedTime(float elapsedTime)
    {
        _elapsedTime.text = elapsedTime.ToString("F2");
    }
}
