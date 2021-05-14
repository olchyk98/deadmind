using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPadButton : Interactable
{
    [SerializeField]
    int keyIndex;
    public UnityAction<int> OnKeyPress;
    private Animation _animation;
    private AudioSource _audioSource;

    private void Start()
    {
        OnInteract += PressKey;
        _animation = GetComponentInChildren<Animation>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    void PressKey()
    {
        OnKeyPress?.Invoke(keyIndex);
        _animation.Play();
        _audioSource.Play();
    }
    private void OnDestroy()
    {
        OnInteract -= PressKey;
    }
}
