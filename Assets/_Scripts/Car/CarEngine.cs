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

        EventBus.Register(EventBus.EventType.PauseGame, PauseEngineAudio);
        EventBus.Register(EventBus.EventType.UnpauseGame, UnpauseEngineAudio);
    }

    private void OnDisable()
    {
        CarData.Velocity.OnChange -= OnCarVelocityChange;

        EventBus.Unregister(EventBus.EventType.PauseGame, PauseEngineAudio);
        EventBus.Unregister(EventBus.EventType.UnpauseGame, UnpauseEngineAudio);
    }

    private void OnCarVelocityChange(float velocity)
    {
        var percentFromMaxVelocity = velocity / CarData.MaxCombinedVelocity;
        var enginePitch = Mathf.Lerp(_pitchRange.x, _pitchRange.y, _pitchCurve.Evaluate(percentFromMaxVelocity));
        _engine.pitch = enginePitch;
    }

    private void PauseEngineAudio(object obj)
    {
        _engine.Pause();
    }

    private void UnpauseEngineAudio(object obj)
    {
        _engine.UnPause();
    }
}
