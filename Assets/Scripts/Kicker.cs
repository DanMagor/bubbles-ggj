using System;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _rotationAngle;
    private float _timer;
    private Quaternion _initRotation;
    private Quaternion _targetRotation;
    [SerializeField] private float _rotateSpeed;

    private void Start()
    {
        _timer = 0;
        _initRotation = transform.rotation;
        _targetRotation = _initRotation * Quaternion.Euler(Vector3.up * _rotationAngle);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (!(_timer >= _cooldown)) return;
        
        if (Quaternion.Angle(transform.rotation, _targetRotation) <= 1)
        {
            _timer = 0f;
            _initRotation = transform.rotation;
            _targetRotation = _initRotation * Quaternion.Euler(Vector3.up * _rotationAngle);
        }
        else
        {
            transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
        }
    }
}