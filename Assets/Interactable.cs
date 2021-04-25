using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public string interactText;
    public UnityAction OnInteract;
    public virtual void Hit()
    {
        OnInteract?.Invoke();
    }
}
