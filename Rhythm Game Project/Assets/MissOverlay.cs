namespace GameplayScripts
{
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class MissOverlay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;

        public void PlayOverlayTween()
        {
            LeanTween.cancel(canvasGroup.gameObject);
            canvasGroup.alpha = 0;
            LeanTween.alphaCanvas(canvasGroup, 0.1f, 2f).setEasePunch();
        }
    }
}