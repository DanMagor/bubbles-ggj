using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")] [SerializeField]
    private float _acceleration = 5f;

    [SerializeField] private float _maxSpeed = 10f;
    //Can be changed depending on what material ball is moving
    public float friction = 2f;
    public float turnResponsiveness = 2f;

    [Header("Physics Settings")] [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField] private Rigidbody _rigidbody;
    private Vector2 _moveInput;
    private Vector3 _velocity;

    private void FixedUpdate()
    {
        var inputVelocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _acceleration;

        if (_moveInput != Vector2.zero)
            _velocity = Vector3.Lerp(_velocity, inputVelocity, turnResponsiveness * Time.fixedDeltaTime);
        else
            _velocity = Vector3.Lerp(_velocity, Vector3.zero, friction * Time.fixedDeltaTime);

        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);
        if (_velocity.magnitude > 0.1f)
        {
            var rotation = Quaternion.Euler(new Vector3(_velocity.z, 0, -_velocity.x) * _rotationSpeed);
            _rigidbody.MoveRotation(rotation * _rigidbody.rotation);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}