using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button3D : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnPress;
    Animation animation;
    private void Start()
    {
        animation = GetComponent<Animation>();
    }
    private void OnMouseDown()
    {
        OnPress.Invoke();
        if(animation != null)animation.Play();
    }
}
