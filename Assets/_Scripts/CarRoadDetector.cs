using System.Collections.Generic;
using UnityEngine;

public class CarRoadDetector : MonoBehaviour
{
    private readonly Dictionary<Collider, Road> _cachedRoads = new Dictionary<Collider, Road>();

    private RaycastHit[] _hitResults = new RaycastHit[1];
    private int _roadLayerMask;

    private void Awake()
    {
        _roadLayerMask = LayerMask.GetMask("Road");
    }

    public float GetMaxVelocityMultiplier(Transform carTransform)
    {
        var ray = new Ray(carTransform.position, -carTransform.up);
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

    public void ClearRoadCache()
    {
        _cachedRoads.Clear();
    }
}
