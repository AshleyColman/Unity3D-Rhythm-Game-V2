namespace UIScripts
{
    using System.Collections;
    using UnityEngine;

    public sealed class FlashEffect : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        private IEnumerator playFlashTweenCoroutine;
        private IEnumerator playFlashInTweenCoroutine;

        public void PlayFlashTween()
        {
            if (playFlashTweenCoroutine != null)
            {
                StopCoroutine(playFlashTweenCoroutine);
            }
            playFlashTweenCoroutine = PlayFlashTweenCoroutine();
            StartCoroutine(playFlashTweenCoroutine);
        }
        public void PlayFlashInTween()
        {
            if (playFlashInTweenCoroutine != null)
            {
                StopCoroutine(playFlashInTweenCoroutine);
            }
            playFlashInTweenCoroutine = PlayFlashInTweenCoroutine();
            StartCoroutine(playFlashInTweenCoroutine);
        }
        private IEnumerator PlayFlashTweenCoroutine()
        {
            LeanTween.cancel(canvasGroup.gameObject);
            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(true);
            LeanTween.alphaCanvas(canvasGroup, 1f, 0.2f).setLoopPingPong(1);
            yield return new WaitForSeconds(0.4f);
            canvasGroup.gameObject.SetActive(false);
            yield return null;
        }
        private IEnumerator PlayFlashInTweenCoroutine()
        {
            LeanTween.cancel(canvasGroup.gameObject);
            canvasGroup.alpha = 1f;
            canvasGroup.gameObject.SetActive(true);
            LeanTween.alphaCanvas(canvasGroup, 0f, 1f).setEaseOutExpo();
            yield return new WaitForSeconds(1f);
            canvasGroup.gameObject.SetActive(false);
            yield return null;
        }
    }
}