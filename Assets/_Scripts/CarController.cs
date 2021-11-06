#pragma warning disable 0649
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigibody;
    [SerializeField] private Transform _carTransform;
    [SerializeField] [Range(0.1f, 10f)] private float _acceleration = 2f;
    [SerializeField] [Range(10f, 100f)] private float _maxVelocity = 20f;
    [SerializeField] [Range(0.01f, 1f)] private float _turnVelocity = 0.1f;

    private float _velocity;

    private void FixedUpdate()
    {
        _velocity += _acceleration * Time.fixedDeltaTime;
        _velocity = Mathf.Clamp(_velocity, 0, _maxVelocity);
        _carRigibody.velocity = _carTransform.forward * _velocity;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        _carTransform.Rotate(Vector3.up, horizontal * _turnVelocity);
    }
}
