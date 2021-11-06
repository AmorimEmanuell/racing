#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigibody;
    [SerializeField] private Transform _carTransform;
    [SerializeField] [Range(0.1f, 10f)] private float _acceleration = 2f;
    [SerializeField] [Range(10f, 100f)] private float _maxVelocity = 20f;
    [SerializeField] [Range(0.01f, 1f)] private float _turnVelocity = 0.1f;

    private float _velocity;
    private RaycastHit[] _hitResults = new RaycastHit[1];
    private Dictionary<Collider, Road> _cachedRoads = new Dictionary<Collider, Road>();
    private int _roadLayerMask;

    private void Awake()
    {
        _roadLayerMask = LayerMask.GetMask("Road");
    }

    private void FixedUpdate()
    {
        var maxVelocityMultiplier = GetMaxVelocityMultiplier();
        _velocity += _acceleration * Time.fixedDeltaTime;
        _velocity = Mathf.Clamp(_velocity, 0, _maxVelocity * maxVelocityMultiplier);
        _carRigibody.velocity = _carTransform.forward * _velocity;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        _carTransform.Rotate(Vector3.up, horizontal * _turnVelocity);
    }

    public void ObstacleHit()
    {
        _velocity /= 2f;
    }

    private float GetMaxVelocityMultiplier()
    {
        var ray = new Ray(_carTransform.position, -_carTransform.up);
        var hits = Physics.RaycastNonAlloc(ray, _hitResults, 1f, _roadLayerMask);
        if (hits == 0)
        {
            return 1f;
        }

        var hit = _hitResults[0];
        if (_cachedRoads.TryGetValue(hit.collider, out Road road))
        {
            return road.MaxVelocityMultiplier;
        }

        var newlyDiscoveredRoad = hit.collider.GetComponent<Road>();
        _cachedRoads.Add(hit.collider, newlyDiscoveredRoad);
        return newlyDiscoveredRoad.MaxVelocityMultiplier;
    }
}
