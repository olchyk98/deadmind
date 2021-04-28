using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPadButton : Interactable
{
    [SerializeField]
    int keyIndex;
    public UnityAction<int> OnKeyPress;

    private void Start()
    {
        OnInteract += PressKey;
    }
    void PressKey()
    {
        OnKeyPress?.Invoke(keyIndex);
    }
    private void OnDestroy()
    {
        OnInteract -= PressKey;
    }
}
