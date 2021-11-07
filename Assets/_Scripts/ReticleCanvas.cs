#pragma warning disable 0649
using UnityEngine;

public class ReticleCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _reticle;

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.GamePaused, OnGamePaused);
        EventBus.Register(EventBus.EventType.GameUnpaused, OnGameUnpaused);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.GamePaused, OnGamePaused);
        EventBus.Unregister(EventBus.EventType.GameUnpaused, OnGameUnpaused);
    }

    private void OnGamePaused()
    {
        _reticle.SetActive(true);
    }

    private void OnGameUnpaused()
    {
        _reticle.SetActive(false);
    }
}
