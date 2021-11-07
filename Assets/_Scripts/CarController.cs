#pragma warning disable 0649
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Parts")]
    [SerializeField] private Rigidbody _carRigibody;
    [SerializeField] private Transform _carTransform;
    
    [Header("Velocity Parameters")]
    [SerializeField] [Range(0.1f, 10f)] private float _acceleration = 2f;
    [SerializeField] [Range(10f, 100f)] private float _maxVelocity = 20f;
    [SerializeField] [Range(0.01f, 1f)] private float _steeringVelocity = 0.1f;

    [Space(10)]
    [SerializeField] private CarBoost _carBoost;
    [SerializeField] private CarRoadDetector _carRoadDetector;

    private void Awake()
    {
        CarData.MaxCombinedVelocity = _maxVelocity + _carBoost.MaxVelocityIncrease;
    }

    private void OnEnable()
    {
        _carBoost.OnEngage += EngageBoost;
        _carBoost.OnDisengage += DisengageBoost;

        EventBus.Register(EventBus.EventType.ResetCar, OnResetCar);
    }

    private void OnDisable()
    {
        _carBoost.OnEngage -= EngageBoost;
        _carBoost.OnDisengage -= DisengageBoost;

        EventBus.Unregister(EventBus.EventType.ResetCar, OnResetCar);
    }

    private void FixedUpdate()
    {
        var velocity = CarData.Velocity.Get() + _acceleration * Time.fixedDeltaTime;
        var maxVelocity = _maxVelocity * _carRoadDetector.GetMaxVelocityMultiplier(_carTransform);

        if (velocity > maxVelocity)
        {
            velocity = Mathf.Lerp(velocity, maxVelocity, Time.fixedDeltaTime);
        }

        _carRigibody.velocity = _carTransform.forward * velocity;

        CarData.Velocity.Set(velocity);
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        _carTransform.Rotate(Vector3.up, horizontal * _steeringVelocity);
    }

    public void ObstacleHit()
    {
        CarData.Velocity.Set(CarData.Velocity.Get() / 2f);
    }

    private void OnResetCar(object obj)
    {
        CarData.Velocity.Set(0);
        _carRigibody.velocity = Vector3.zero;

        var startPosition = obj as Transform;
        _carTransform.rotation = startPosition.rotation;
        _carTransform.position = startPosition.position;

        _carRoadDetector.ClearRoadCache();
    }

    private void EngageBoost(float accelerationBoost, float maxVelocityIncrease, float steeringDificultyMultiplier)
    {
        _steeringVelocity /= steeringDificultyMultiplier;
        _maxVelocity += maxVelocityIncrease;
        _acceleration += accelerationBoost;
    }

    private void DisengageBoost(float accelerationBoost, float maxVelocityIncrease, float steeringDificultyMultiplier)
    {
        _acceleration -= accelerationBoost;
        _maxVelocity -= maxVelocityIncrease;
        _steeringVelocity *= steeringDificultyMultiplier;
    }
}
