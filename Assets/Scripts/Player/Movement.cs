using System;
using Player;
using UnityEngine;

[RequireComponent(typeof(InputActionHandler))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isGrounded;
    private AudioSource _audioSource;
    [SerializeField] [Range(1f, 20f)] private float _movementSpeed;
    [SerializeField] [Range(1f, 20f)] private float _jumpHeight;
    [SerializeField] private string _groundTag;

    private Transform _transform;

    private void Start()
    {
        var input = GetComponent<InputActionHandler>();
        input.OnMove += OnMove;

        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMove(Vector3 input)
    {
        const float extraForce = 10f;

        var vertical = _transform.forward * (input.x * _movementSpeed * extraForce);
        var horizontal = _transform.right * (input.z * _movementSpeed * extraForce);

        _rb.velocity = vertical + horizontal + _transform.up * _rb.velocity.y;
        if(_rb.velocity.x != 0 || _rb.velocity.z != 0)
        {
            _audioSource.enabled = true;
        }
        else
        {
            _audioSource.enabled = false;
        }
        if (input.y > 0 && _isGrounded)
        {
            Jump(input.y);
        }
    }

    private void Jump(float input)
    {
        _rb.AddForce(Vector3.up * (_jumpHeight * input), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = false;
        }
    }
}
