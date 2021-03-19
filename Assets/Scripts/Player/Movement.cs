using System;
using System.Collections;
using System.Collections.Generic;
using Player.Input;
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
        input.OnRotate += OnRotate;
        
        rb = GetComponent<Rigidbody>();
    }

    private void OnRotate(Vector3 rotation)
    {
        
    }

    private void OnMove(Vector3 input)
    {
        
    }
}
