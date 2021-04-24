using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public string interactText;
    Collider objectCollider;
    [SerializeField]
    UnityEvent interactEvent;
    private void Start()
    {
        objectCollider = GetComponent<Collider>();
    }
    public void Hit()
    {

         interactEvent.Invoke();
    }
}
