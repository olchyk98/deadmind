using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Door : Interactable
{
    Animator animator;
    private void Start()
    {
        OnInteract += ToggleState;
        animator = GetComponent<Animator>();
    }
    public void ToggleState()
    {
        animator.SetBool("isOpen", !animator.GetBool("isOpen"));
    }
    private void OnDestroy()
    {
        OnInteract -= ToggleState;
    }
}
