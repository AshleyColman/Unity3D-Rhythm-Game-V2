namespace StartMenuScripts
{
    using UnityEngine;

    public sealed class FadeText : EffectText
    {
        [SerializeField] private CanvasGroup canvasGroup = default;

        public void PlayAlphaCanvasTween(float _time, byte _loops) => LeanTween.alphaCanvas(canvasGroup, 0f, _time).setLoopPingPong(_loops);
        public void PlayAlphaCanvasTweenLoop() => LeanTween.alphaCanvas(canvasGroup, 0f, 1f).setLoopPingPong(-1);
        public void StopAlphaCanvasTween() => LeanTween.cancel(canvasGroup.gameObject);
    }
}