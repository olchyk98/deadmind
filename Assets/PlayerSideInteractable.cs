using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerSideInteractable : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        Physics.Raycast(ray, out var hit, 5);

        Interactable interactable = hit.collider?.gameObject.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactable.Hit();
        }
    }
}
