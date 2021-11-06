using TMPro;
using UnityEngine;

public class VelocityPanel : MonoBehaviour
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
