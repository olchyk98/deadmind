using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        [Range(1f, 200f)] [SerializeField] private float _mouseSensitivity;
        [SerializeField] private Transform _cameraTransform;
        private Transform _transform;
        private InputActionHandler _input;

        private float _rotationY = 0f;

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _input = GetComponent<InputActionHandler>();
        }

        private void Update()
        {
            HandleLookTick();
        }

        private void HandleLookTick ()
        {
            var mouseDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            var horizontalRotation = _transform.rotation.eulerAngles;
            var nextY = _rotationY - mouseDirection.y * _mouseSensitivity;

            _rotationY = Mathf.Clamp(nextY, -90f, 90f);
            horizontalRotation.y += mouseDirection.x * _mouseSensitivity;

            // Apply rotation changes
            _cameraTransform.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
            _transform.rotation = Quaternion.Euler(horizontalRotation);
        }
    }
}
