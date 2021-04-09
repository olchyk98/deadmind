using Player;
using System;
using Player;
using UnityEngine;

[RequireComponent(typeof(InputActionHandler))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private bool grounded;

    [SerializeField] [Range(1f, 20f)] private float movementSpeed;
    [SerializeField] [Range(1f, 20f)] private float jumpHeight;
    [SerializeField] private string groundTag;

    private void Start()
    {
        var input = GetComponent<InputActionHandler>();
        input.OnMove += OnMove;

        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(Vector3 input)
    {
        var jumping = input.y;
        var movement = new Vector3(input.x*movementSpeed*10, rb.velocity.y, input.z*movementSpeed*10);
        
        rb.velocity = movement;
        if (jumping > 0 && grounded)
        {
            Jump(jumping);
        }

    }

    private void Jump(float input)
    {
        rb.AddForce(Vector3.up * (jumpHeight * input), ForceMode.Impulse);
    }

   

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            grounded = false;
        }
    }
}
