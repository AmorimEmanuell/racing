#pragma warning disable 0649
using System.Collections.Generic;
using UnityEngine;

public class CarRoadDetector : MonoBehaviour
{
    [SerializeField] private Wheel[] _wheels;

    private readonly Dictionary<Collider, Road> _cachedRoads = new Dictionary<Collider, Road>();

    private RaycastHit[] _hitResults = new RaycastHit[1];
    private int _roadLayerMask;

    private void Awake()
    {
        _roadLayerMask = LayerMask.GetMask("Road");
    }

    public float GetRoadMultipler()
    {
        var roadMultiplier = 1f;
        for (var i = 0; i < _wheels.Length; i++)
        {
            var currentWheel = _wheels[i];
            var ray = new Ray(currentWheel.Transform.position, -currentWheel.Transform.up);
            var hits = Physics.RaycastNonAlloc(ray, _hitResults, 1f, _roadLayerMask);
            if (hits == 0)
            {
                currentWheel.EnableDirtEffect(false);
                continue;
            }

            var hit = _hitResults[0];
            if (_cachedRoads.TryGetValue(hit.collider, out Road road))
            {
                if (road.MaxVelocityMultiplier < roadMultiplier)
                {
                    roadMultiplier = road.MaxVelocityMultiplier;
                }

                CheckDirtRoad(currentWheel, road);
                continue;
            }

            var newlyDiscoveredRoad = hit.collider.GetComponent<Road>();
            _cachedRoads.Add(hit.collider, newlyDiscoveredRoad);
            if (newlyDiscoveredRoad.MaxVelocityMultiplier < roadMultiplier)
            {
                roadMultiplier = newlyDiscoveredRoad.MaxVelocityMultiplier;
            }

            CheckDirtRoad(currentWheel, newlyDiscoveredRoad);
        }

        return roadMultiplier;
    }

    private void CheckDirtRoad(Wheel wheel, Road road)
    {
        wheel.EnableDirtEffect(road.IsDirt());
    }

    public void ClearRoadCache()
    {
        _cachedRoads.Clear();
    }
}
