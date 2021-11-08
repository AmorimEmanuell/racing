#pragma warning disable 0649
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 10f)] private float _maxVelocityMultiplier = 1f;
    [SerializeField] private RoadType _roadType;

    public enum RoadType
    {
        Asphalt,
        Dirt
    }

    public float MaxVelocityMultiplier => _maxVelocityMultiplier;

    public bool IsDirt()
    {
        return _roadType == RoadType.Dirt;
    }
}
