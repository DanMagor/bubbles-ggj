using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnitchLogic : MonoBehaviour
{
    private Vector3 _targetPos;
    [SerializeField] private float _speed = 10f;
    private Bounds _bounds;
    [SerializeField] private Collider _boxArea;

    private void Start()
    {
        GetRandomTarget();
        _bounds = _boxArea.bounds;
    }

    private void Update()
    {
        if (Vector3.Distance(_targetPos, transform.position) < 0.1f) _targetPos = GetRandomTarget();

        transform.position += (_targetPos - transform.position).normalized * (_speed * Time.deltaTime);
    }

    private Vector3 GetRandomTarget()
    {
        return new Vector3(
            Random.Range(_bounds.min.x, _bounds.max.x),
            0.5f,
            Random.Range(_bounds.min.z, _bounds.max.z)
        );
    }
}