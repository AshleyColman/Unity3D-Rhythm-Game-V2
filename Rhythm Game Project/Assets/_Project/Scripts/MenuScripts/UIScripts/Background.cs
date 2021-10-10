namespace UIScripts
{
    using StaticDataScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class Background : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private Image image = default;
        private Transform imageTransform;

        public void TransitionIn(float _duration = 1f) => LeanTween.alphaCanvas(canvasGroup, 1f, _duration);
        public void TransitionOut(float _duration = 1f) => LeanTween.alphaCanvas(canvasGroup, 0f, _duration);
        public void ScaleImage(float _duration = 1f)
        {
            LeanTween.cancel(imageTransform.gameObject);
            imageTransform.localScale = Vector3.one;
            LeanTween.scale(imageTransform.gameObject, VectorValues.Vector1_01, _duration).setEaseOutExpo();
        }
        private void Awake() => imageTransform = image.transform;
    }
}