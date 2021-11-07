#pragma warning disable 0649
using UnityEngine;

public class ReticleCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _reticle;

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.PauseGame, OnGamePaused);
        EventBus.Register(EventBus.EventType.UnpauseGame, OnGameUnpaused);
        EventBus.Register(EventBus.EventType.RestartGame, OnGameRestarted);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.PauseGame, OnGamePaused);
        EventBus.Unregister(EventBus.EventType.UnpauseGame, OnGameUnpaused);
        EventBus.Register(EventBus.EventType.RestartGame, OnGameRestarted);
    }

    private void OnGamePaused(object args)
    {
        _reticle.SetActive(true);
    }

    private void OnGameUnpaused(object obj)
    {
        _reticle.SetActive(false);
    }

    private void OnGameRestarted(object obj)
    {
        _reticle.SetActive(false);
    }
}
