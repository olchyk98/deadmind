using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPadButton : Interactable
{
    [SerializeField]
    int keyIndex;
    public UnityAction<int> keyPress;

    public override void Hit()
    {
        base.Hit();
        keyPress?.Invoke(keyIndex);
    }
}
