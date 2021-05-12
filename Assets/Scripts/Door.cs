using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Door : Interactable
{
    private Animator _animator;
    [SerializeField]
    private AudioClip[] _audioClips = new AudioClip[2];
    private AudioSource _audioSource;
    private void Start()
    {
        OnInteract += ToggleState;
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void ToggleState()
    {
        _animator.SetBool("isOpen", !_animator.GetBool("isOpen"));
        _audioSource.clip = _audioClips[Convert.ToInt32(_animator.GetBool("isOpen"))];
        _audioSource.Play();
    }
    private void OnDestroy()
    {
        OnInteract -= ToggleState;
    }
}
