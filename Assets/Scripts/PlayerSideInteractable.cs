using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerSideInteractable : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TextMeshProUGUI _interactText;
    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out var hit, 10f);
        Interactable interactable = hit.collider?.gameObject.GetComponent<Interactable>();
        _interactText.text = interactable?.interactText;

        if (interactable != null && interactable.CanInteract)
        {
            _interactText.text = interactable.interactText;
            if(Input.GetKeyDown(KeyCode.E))
            {
                interactable.Hit();
            }

            return;
        }

        _interactText.text = "";
    }
}
