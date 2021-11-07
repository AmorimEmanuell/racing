#pragma warning disable 0649
using System;
using UnityEngine;

public class CarBoost : MonoBehaviour
{
    [SerializeField] [Range(5f, 20f)] private float _rechargeTime = 10f;
    [SerializeField] [Range(2.5f, 10f)] private float _usageTime = 5f;
    [SerializeField] [Range(1f, 10f)] private float _accelerationBoost = 2f;
    [SerializeField] [Range(5f, 20f)] private float _maxVelocityIncrease = 10f;
    [SerializeField] [Range(1.1f, 2f)] private float _steeringDificultyMultiplier = 1.5f;

    private bool _isBoosting;

    public float MaxVelocityIncrease => _maxVelocityIncrease;

    public Action<float, float, float> OnEngage, OnDisengage;

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.EngageCarBoost, OnEngageBoost);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.EngageCarBoost, OnEngageBoost);
    }

    private void Update()
    {
        if (!_isBoosting)
        {
            var boost = CarData.Boost.Get() + Time.deltaTime / _rechargeTime;
            boost = Mathf.Clamp01(boost);
            CarData.Boost.Set(boost);
        }
        else
        {
            var boost = CarData.Boost.Get() - Time.deltaTime / _usageTime;
            boost = Mathf.Clamp01(boost);
            CarData.Boost.Set(boost);

            if (boost == 0)
            {
                _isBoosting = false;
                OnDisengage?.Invoke(_accelerationBoost, _maxVelocityIncrease, _steeringDificultyMultiplier);
            }
        }
    }

    private void OnEngageBoost(object obj)
    {
        if (_isBoosting)
        {
            return;
        }

        if (CarData.Boost.Get() == 1)
        {
            _isBoosting = true;
            OnEngage?.Invoke(_accelerationBoost, _maxVelocityIncrease, _steeringDificultyMultiplier);
        }
    }
}
