#pragma warning disable 0649
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VelocityPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _velocity;
    [SerializeField] private Slider _boostSlider;

    private void OnEnable()
    {
        CarData.Velocity.OnChange += UpdateVelocity;
        CarData.Boost.OnChange += UpdateBoostSlider;
    }

    private void OnDisable()
    {
        CarData.Velocity.OnChange -= UpdateVelocity;
        CarData.Boost.OnChange -= UpdateBoostSlider;
    }

    private void UpdateVelocity(float velocity)
    {
        _velocity.SetText("{0:2}", velocity);
    }

    private void UpdateBoostSlider(float boost)
    {
        _boostSlider.value = boost;
    }
}
