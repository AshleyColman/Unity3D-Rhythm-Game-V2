namespace UIScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using StaticDataScripts;

    public sealed class Notification : MonoBehaviour
    {
        private readonly Vector3 DefaultLocalScale = new Vector3(1f, 0f, 1f);
        [SerializeField] private Transform notification = default;
        [SerializeField] private Image colorImage = default;
        [SerializeField] private TextMeshProUGUI titleText = default;
        [SerializeField] private TextMeshProUGUI descriptionText = default;
        [SerializeField] private FlashEffect flashEffect = default;
        private IEnumerator showCoroutine;
        private IEnumerator transitionOutCoroutine;

        public void Show(Color32 _color, string _title, string _description, float _duration = 4f)
        {
            if (showCoroutine != null)
            {
                StopCoroutine(showCoroutine);
            }
            StopTransitionOut();
            showCoroutine = ShowCoroutine(_color, _title, _description, _duration);
            StartCoroutine(showCoroutine);
        }
        private IEnumerator ShowCoroutine(Color32 _color, string _title, string _description, float _duration)
        {
            titleText.SetText(_title.ToUpper());
            descriptionText.SetText(_description.ToUpper());
            colorImage.color = _color;
            TransitionIn();
            yield return new WaitForSeconds(_duration);
            TransitionOut();
            yield return null;
        }
        private void TransitionIn()
        {
            LeanTween.cancel(notification.gameObject);
            notification.localScale = DefaultLocalScale;
            notification.gameObject.SetActive(true);
            LeanTween.scaleY(notification.gameObject, 1f, 0.1f);
            flashEffect.PlayFlashTween();
        }
        private void TransitionOut()
        {
            transitionOutCoroutine = TransitionOutCoroutine();
            StartCoroutine(transitionOutCoroutine);
        }
        private void StopTransitionOut()
        {
            if (transitionOutCoroutine != null)
            {
                StopCoroutine(transitionOutCoroutine);
            }
        }
        private IEnumerator TransitionOutCoroutine()
        {
            LeanTween.cancel(notification.gameObject);
            LeanTween.scaleY(notification.gameObject, 0f, 0.1f);
            yield return new WaitForSeconds(0.1f);
            notification.gameObject.SetActive(false);
            yield return null;
        }
    }
}