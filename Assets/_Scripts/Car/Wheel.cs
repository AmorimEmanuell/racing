#pragma warning disable 0649
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _transform;

    public Transform Transform => _transform;

    public void EnableDirtEffect(bool enable)
    {
        _particleSystem.gameObject.SetActive(enable);
    }
}
