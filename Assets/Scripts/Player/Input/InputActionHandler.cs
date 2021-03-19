using UnityEngine;
using UnityEngine.Events;
using UInput = UnityEngine.Input;

namespace Player
{
    public class InputActionHandler : MonoBehaviour
    {
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
            float y = UInput.GetAxisRaw("Vertical");
            float z = UInput.GetAxisRaw("Horizontal");
            float x = UInput.GetAxis("Jump");
            x *= Time.fixedDeltaTime;
            y *= Time.fixedDeltaTime;
            z *= Time.fixedDeltaTime;
            OnMove?.Invoke(new Vector3(x, y, z));
        }

        private void HandleRotationTick()
        {
            var v = new Vector3(UInput.GetAxis("Mouse Y"), UInput.GetAxis("Mouse X"), 0) * Time.fixedDeltaTime;
            OnRotate?.Invoke(v);
        }
    }
}
