using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerSideInteractable : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    TextMeshProUGUI interactText;
    RaycastHit? raycastHit;
    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Physics.Raycast(ray, out var hit, 5);
        raycastHit = hit;
        Interactable interactable = raycastHit?.collider?.gameObject?.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactText.text = interactable.interactText;
            interactable.Hit();
        }
        else
        {
            interactText.text = "";
        }
    }
}
