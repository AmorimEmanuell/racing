#pragma warning disable 0649
using UnityEngine;

public class ReticleCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _reticle;

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.PauseGame, Show);
        EventBus.Register(EventBus.EventType.UnpauseGame, Hide);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.PauseGame, Show);
        EventBus.Unregister(EventBus.EventType.UnpauseGame, Hide);
    }

    private void Show(object args)
    {
        _reticle.SetActive(true);
    }

    private void Hide(object obj)
    {
        _reticle.SetActive(false);
    }
}
