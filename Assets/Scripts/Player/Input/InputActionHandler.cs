using UnityEngine;
using UnityEngine.Events;
using UInput = UnityEngine.Input;

namespace Player.Input
{
    public class InputActionHandler : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float povSpeed;
        [SerializeField] private float jumpHeight;
        
        public UnityAction<Vector3> OnMove;
        public UnityAction<Vector3> OnRotate;

        // Update is called once per frame
        private void FixedUpdate()
        {
            HandleMovementTick();
            HandleRotationTick();
        }

        private void HandleMovementTick()
        {
            float y = UInput.GetAxisRaw("Vertical") * movementSpeed;
            float z = UInput.GetAxisRaw("Horizontal") * movementSpeed;
            float x = UInput.GetAxis("Jump") * jumpHeight;
            x *= Time.fixedDeltaTime;
            y *= Time.fixedDeltaTime;
            z *= Time.fixedDeltaTime;
            OnMove?.Invoke(new Vector3(x, y, z));
        }

        private void HandleRotationTick()
        {
            var v = new Vector3(UInput.GetAxis("Mouse Y"), UInput.GetAxis("Mouse X"), 0) * (Time.deltaTime * povSpeed);
            OnRotate?.Invoke(v);
        }
    }
}