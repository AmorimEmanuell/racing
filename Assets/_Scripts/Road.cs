using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 10f)] private float _maxVelocityMultiplier = 1f;

    public float MaxVelocityMultiplier => _maxVelocityMultiplier;
}
