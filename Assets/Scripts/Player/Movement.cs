using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(InputActionHandler))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float povSpeed;
    [SerializeField] private float jumpHeight;

    private void Start()
    {
        var input = GetComponent<InputActionHandler>();
        input.OnMove += OnMove;

        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(Vector3 input)
    {

    }
}
