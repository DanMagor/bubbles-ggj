using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private string _iceTag = "Ice";

    [Header("Movement Settings")] [SerializeField]
    private float _acceleration = 5f;

    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _baseStunDuration = 0.2f;
    [SerializeField] private float _additionalStunFor1MassDiff = 0.1f;
    private Vector2 _moveInput;


    [Header("Physics Settings")] [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _velocity;
    [SerializeField] private PhotonView photonView;

    //Can be changed depending on what material ball is moving
    public float friction = 2f;
    public float frictionOnIce = 0.5f;
    public float turnResponsiveness = 2f;
    public float turnResponsivenessOnIce = 1f;
    private bool _isStunned = false;
    private float _baseFriction = 0f;
    private float _baseTurnResponsiveness = 0f;
    [SerializeField] private bool _inverseControl = false;
    [SerializeField] private bool _isAlive = true;

    private void Awake()
    {
        _baseFriction = friction;
        _baseTurnResponsiveness = turnResponsiveness;
    }

    private void FixedUpdate()
    {
        var control = 1f;
        if (_inverseControl)
        {
            control = -1f;
        }

        if (!_isStunned && _isAlive)
        {
            var inputVelocity = new Vector3(_moveInput.x * control * turnResponsiveness, 0,
                                    _moveInput.y * control * turnResponsiveness) *
                                _acceleration;

            _rigidbody.AddForce(inputVelocity, ForceMode.Acceleration);

            var horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

            if (horizontalVelocity.magnitude > _maxSpeed)
            {
                horizontalVelocity = horizontalVelocity.normalized * _maxSpeed;
                _rigidbody.velocity = new Vector3(horizontalVelocity.x, _rigidbody.velocity.y, horizontalVelocity.z);
            }

            if (_moveInput == Vector2.zero)
            {
                var newHorizontalVelocity =
                    Vector3.Lerp(horizontalVelocity, Vector3.zero, friction * Time.fixedDeltaTime);
                _rigidbody.velocity =
                    new Vector3(newHorizontalVelocity.x, _rigidbody.velocity.y, newHorizontalVelocity.z);
            }
        }

        if (!_isAlive)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void InverseControl()
    {
        _inverseControl = true;
    }

    public void ResetInverse()
    {
        _inverseControl = false;
    }

    public void KillPlayer()
    {
        _isAlive = false;
    }

    public void RessurectPlayer()
    {
        _isAlive = true;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(_playerTag) && !_isStunned)
        {
            var enemyRb = col.gameObject.GetComponent<Rigidbody>();
            if (enemyRb.mass > _rigidbody.mass)
            {
                var massDiff = enemyRb.mass - _rigidbody.mass;
                _isStunned = true;
                StartCoroutine(Stun(massDiff * _additionalStunFor1MassDiff));
            }
            else
            {
                StartCoroutine(Stun(0f));
            }
        }

        if (col.gameObject.CompareTag(_iceTag))
        {
            friction = frictionOnIce;
            turnResponsiveness = turnResponsivenessOnIce;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(_iceTag))
        {
            friction = _baseFriction;
            turnResponsiveness = _baseTurnResponsiveness;
        }
    }

    private IEnumerator Stun(float additionalStunDuration)
    {
        yield return new WaitForSeconds(_baseStunDuration + additionalStunDuration);
        _isStunned = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!photonView.IsMine) return;
        _moveInput = context.ReadValue<Vector2>();
    }
}