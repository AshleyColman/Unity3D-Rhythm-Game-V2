namespace UIScripts
{
    using UnityEngine;

    public sealed class Cursor : MonoBehaviour
    {
        [SerializeField] private GameObject cursor = default;
        private Transform cursorTransform;
        private Vector3 prevFramePosition;

        private void Awake()
        {
            //UnityEngine.Cursor.visible = false;
            cursorTransform = cursor.transform;
        }
        private void Update()
        {
            if (Input.mousePosition != prevFramePosition)
            {
                SetPosition();
            }
            SetPrevFramePosition();
        }
        private void SetPosition() => cursorTransform.position = Input.mousePosition;
        private void SetPrevFramePosition() => prevFramePosition = Input.mousePosition;
    }
}