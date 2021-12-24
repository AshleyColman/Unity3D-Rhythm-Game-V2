namespace GameplayScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class ColorOverlay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private Image colorImage = default;
        public void PlayOverlayTween(Color32 _color)
        {
            colorImage.color = _color;
            LeanTween.cancel(canvasGroup.gameObject);
            canvasGroup.alpha = 0;
            LeanTween.alphaCanvas(canvasGroup, 0.1f, 1f).setEasePunch();
        }
    }
}
