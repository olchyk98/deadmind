using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerSideInteractable : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private TextMeshProUGUI interactText;

    private void Update()
    {

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Physics.Raycast(ray, out var hit, 5);

        Interactable interactable = hit.collider?.gameObject.GetComponent<Interactable>();
        interactText.text = interactable?.interactText;

        if (!Input.GetKeyDown(KeyCode.E)) return;

        interactable?.Hit();
    }
}
