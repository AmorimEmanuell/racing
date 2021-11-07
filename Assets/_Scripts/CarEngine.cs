#pragma warning disable 0649
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    [SerializeField] private AudioSource _engine;
    [SerializeField] private Vector2 _pitchRange;
    [SerializeField] private AnimationCurve _pitchCurve;

    private void OnEnable()
    {
        CarData.Velocity.OnChange += OnCarVelocityChange;
    }

    private void OnDisable()
    {
        CarData.Velocity.OnChange -= OnCarVelocityChange;
    }

    private void OnCarVelocityChange(float velocity)
    {
        var percentFromMaxVelocity = velocity / CarData.MaxCombinedVelocity;
        var enginePitch = Mathf.Lerp(_pitchRange.x, _pitchRange.y, _pitchCurve.Evaluate(percentFromMaxVelocity));
        _engine.pitch = enginePitch;
    }
}
