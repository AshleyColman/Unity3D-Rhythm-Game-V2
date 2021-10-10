namespace UIScripts
{
    using UnityEngine;
    public sealed class FadeTransition : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;

        public void TransitionIn(float _duration = 1f) => LeanTween.alphaCanvas(canvasGroup, 1f, _duration);
        public void TransitionOut(float _duration = 1f) => LeanTween.alphaCanvas(canvasGroup, 0f, _duration);
    }

}