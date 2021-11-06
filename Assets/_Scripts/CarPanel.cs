#pragma warning disable 0649
using UnityEngine;
using TMPro;

public class CarPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _velocity;

    private void OnEnable()
    {
        CarData.Velocity.OnChange += UpdateVelocity;
    }

    private void OnDisable()
    {
        CarData.Velocity.OnChange -= UpdateVelocity;
    }

    private void UpdateVelocity(float velocity)
    {
        _velocity.text = velocity.ToString("F2");
    }
}
