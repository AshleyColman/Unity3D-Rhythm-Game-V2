namespace UIScripts
{
    using System.Collections;
    using UnityEngine;
    public sealed class FadeTransition : MonoBehaviour
    {
        [SerializeField] private GameObject fadeTransition = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        private IEnumerator transitionCoroutine;

        public void TransitionIn(float _duration = 1f)
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }
            transitionCoroutine = TransitionCoroutine(0f, _duration);
            StartCoroutine(transitionCoroutine);
        }
        public void TransitionOut(float _duration = 1f)
        {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine);
            }
            transitionCoroutine = TransitionCoroutine(1f, _duration);
            StartCoroutine(transitionCoroutine);
        }
        private IEnumerator TransitionCoroutine(float _to, float _duration)
        {
            fadeTransition.gameObject.SetActive(true);
            LeanTween.alphaCanvas(canvasGroup, _to, _duration);
            yield return new WaitForSeconds(1f);
            fadeTransition.gameObject.SetActive(false);
            yield return null;
        }
    }

}