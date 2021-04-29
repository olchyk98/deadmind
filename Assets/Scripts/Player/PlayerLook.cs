using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerLook : MonoBehaviour
    {
        [Range(1f, 200f)] [SerializeField] private float _mouseSensitivity;
        [SerializeField] private Transform _cameraTransform;
        private Transform _transform;
        private InputActionHandler _input;
        private Rigidbody _rigidbody;

        private float _rotationY = 0f;

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _input = GetComponent<InputActionHandler>();
            _rigidbody = GetComponent<Rigidbody>();

            _input.OnRotate += HandleLookTick;

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void HandleLookTick (Vector2 direction)
        {
            var horizontalRotation = _transform.rotation.eulerAngles;
            var nextY = _rotationY - direction.y * _mouseSensitivity;

            _rotationY = Mathf.Clamp(nextY, -90f, 90f);
            horizontalRotation.y += direction.x * _mouseSensitivity;

            _rigidbody.angularVelocity = Vector3.zero;

            // Apply rotation changes
            _cameraTransform.localRotation = Quaternion.Euler(_rotationY, 0f, 0f);
            _transform.rotation = Quaternion.Euler(horizontalRotation);
        }
    }
}
