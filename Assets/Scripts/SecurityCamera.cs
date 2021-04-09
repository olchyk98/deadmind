using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    private void Update()
    {
        transform.LookAt(targetTransform);
    }
}
