#pragma warning disable 0649
using System;
using UnityEngine;

public class CarBoost : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] [Range(5f, 20f)] private float _rechargeTime = 10f;
    [SerializeField] [Range(2.5f, 10f)] private float _usageTime = 5f;
    [SerializeField] [Range(1f, 10f)] private float _accelerationBoost = 2f;
    [SerializeField] [Range(5f, 20f)] private float _maxVelocityIncrease = 10f;
    [SerializeField] [Range(1.1f, 2f)] private float _steeringDificultyMultiplier = 1.5f;

    [Space(10)]
    [SerializeField] private AudioSource _boostAudio;

    private bool _isBoosting;

    public float MaxVelocityIncrease => _maxVelocityIncrease;

    public Action<float, float, float> OnEngage, OnDisengage;

    private void OnEnable()
    {
        EventBus.Register(EventBus.EventType.PauseGame, PauseBoostAudio);
        EventBus.Register(EventBus.EventType.UnpauseGame, UnpauseBoostAudio);
    }

    private void OnDisable()
    {
        EventBus.Unregister(EventBus.EventType.PauseGame, PauseBoostAudio);
        EventBus.Unregister(EventBus.EventType.UnpauseGame, UnpauseBoostAudio);
    }

    private void Update()
    {
        if (!_isBoosting)
        {
            var boost = CarData.Boost.Get() + Time.deltaTime / _rechargeTime;
            boost = Mathf.Clamp01(boost);
            CarData.Boost.Set(boost);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActivateBoost();
            }
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

    private void PauseBoostAudio(object obj)
    {
        _boostAudio.Pause();
    }

    private void UnpauseBoostAudio(object obj)
    {
        _boostAudio.UnPause();
    }

    private void ActivateBoost()
    {
        if (CarData.Boost.Get() == 1)
        {
            _isBoosting = true;
            _boostAudio.Play();
            OnEngage?.Invoke(_accelerationBoost, _maxVelocityIncrease, _steeringDificultyMultiplier);
        }
    }

    public void ResetValues()
    {
        if (_isBoosting)
        {
            OnDisengage?.Invoke(_accelerationBoost, _maxVelocityIncrease, _steeringDificultyMultiplier);
        }

        _isBoosting = false;
        CarData.Boost.Set(0);
    }
}
