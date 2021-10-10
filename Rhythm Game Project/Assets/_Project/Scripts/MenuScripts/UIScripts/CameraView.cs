namespace UIScripts
{
    using UnityEngine;

    public sealed class CameraView : MonoBehaviour
    {
        [SerializeField] private Transform rotator;
        public const float Sensitivity = 0.01f;
        private const string AxisY = "Mouse Y";
        private const string AxisX = "Mouse X";
        private float newRotationX = 0f;
        private float newRotationY = 0f;
        private Vector3 prevFrameMousePosition;
        private void Update()
        {
            if (Input.mousePosition != prevFrameMousePosition)
            {
                SetRotation();
            }
            SetPrevFrameMousePosition();
        }
        private void SetRotation()
        {
            newRotationX = Mathf.Clamp(rotator.localEulerAngles.x - Input.GetAxis(AxisY) * Sensitivity, 0f, 2f);
            newRotationY = Mathf.Clamp(rotator.localEulerAngles.y + Input.GetAxis(AxisX) * Sensitivity, 0f, 2f);
            rotator.localEulerAngles = new Vector3(newRotationX, newRotationY, 0);
        }
        private void SetPrevFrameMousePosition() => prevFrameMousePosition = Input.mousePosition;
    }
}