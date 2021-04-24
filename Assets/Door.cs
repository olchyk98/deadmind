using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ToggleState()
    {
            animator.SetBool("isOpen", !animator.GetBool("isOpen"));
    }
}
