using UnityEngine;
using UnityEngine.Events;
using UInput = UnityEngine.Input;

namespace Player
{
    public class InputActionHandler : MonoBehaviour
    {
        public UnityAction<Vector3> OnMove;
        public UnityAction<Vector2> OnRotate;

        // Update is called once per frame
        private void FixedUpdate()
        {
            HandleMovementTick();
            HandleRotationTick();
        }

        private void HandleMovementTick()
        {
            float x = UInput.GetAxisRaw("Vertical");
            float z = UInput.GetAxisRaw("Horizontal");
            float y = UInput.GetAxis("Jump");
            x *= Time.fixedDeltaTime;
            z *= Time.fixedDeltaTime;
            OnMove?.Invoke(new Vector3(x, y, z));
        }

        private void HandleRotationTick()
        {
            var direction = new Vector2(
                UInput.GetAxis("Mouse X"),
                UInput.GetAxis("Mouse Y")
            );
            OnRotate?.Invoke(direction);
        }
    }
}
