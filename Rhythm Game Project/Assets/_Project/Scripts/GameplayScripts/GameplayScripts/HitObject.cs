namespace GameplayScripts
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using StaticDataScripts;

    public sealed class HitObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numberText = default;
        [SerializeField] private GameObject objectProperties = default;
        [SerializeField] private Image colorImage = default;
        [SerializeField] private Image approachImage = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private Transform cTransform = default;
        [SerializeField] private JudgementObject judgementObject = default;
        private IEnumerator playHitTweenCoroutine;
        private IEnumerator playMissTweenCoroutine;
        private Vector3 missRotateTo = Vector3.zero;
        private Quaternion startingRotation;
        private bool setStartingRotation = false;
        private float approachTime = 0;

        public void SetJudgement(Judgement _judgement)
        {
            judgementObject.SetObjectProperties(JudgementData.GetJudgementColor(_judgement));
        }
        public void PlayHitTween()
        {
            if (playHitTweenCoroutine != null)
            {
                StopCoroutine(playHitTweenCoroutine);
            }
            playHitTweenCoroutine = PlayHitTweenCoroutine();
            StartCoroutine(playHitTweenCoroutine);
        }
        public void PlayMissTween()
        {
            if (playMissTweenCoroutine != null)
            {
                StopCoroutine(playMissTweenCoroutine);
            }
            playMissTweenCoroutine = PlayMissTweenCoroutine();
            StartCoroutine(playMissTweenCoroutine);
        }
        public void SetObjectProperties(float _approachTime, Vector3 _position, Color32 _color, string _numberText)
        {
            cTransform.SetAsFirstSibling();
            cTransform.localPosition = _position;
            colorImage.color = _color;
            numberText.SetText(_numberText);
            approachTime = _approachTime;
        }
        private void SetMissRotation() => missRotateTo = new Vector3(cTransform.localRotation.x, cTransform.localRotation.y,
                cTransform.localRotation.z - 45f);
        private void SetStartingRotation()
        {
            if (setStartingRotation == false)
            {
                startingRotation = cTransform.localRotation;
                setStartingRotation = true;
            }
        }
        private void ResetProperties()
        {
            SetStartingRotation();
            cTransform.localScale = Vector3.zero;
            approachImage.transform.localScale = Vector3.zero;
            cTransform.localRotation = startingRotation;
            canvasGroup.alpha = 0f;
            objectProperties.gameObject.SetActive(true);
        }
        private void PlayApproachTween()
        {
            LeanTween.scale(cTransform.gameObject, Vector3.one, 0.25f).setEaseOutExpo();
            LeanTween.scale(approachImage.gameObject, Vector3.one, approachTime);
            LeanTween.alphaCanvas(canvasGroup, 1f, approachTime);
        }
        private void CancelTween()
        {
            LeanTween.cancel(cTransform.gameObject);
            LeanTween.cancel(approachImage.gameObject);
            LeanTween.cancel(canvasGroup.gameObject);
        }
        private void ActivateObjectProperties() => objectProperties.SetActive(true);
        private void DeactivateObjectProperties() => objectProperties.SetActive(false);
        private IEnumerator PlayHitTweenCoroutine()
        {
            CancelTween();
            DeactivateObjectProperties();
            judgementObject.Activate();
            LeanTween.alphaCanvas(canvasGroup, 0f, 0.25f).setEaseOutExpo();
            LeanTween.scale(gameObject, VectorValues.Vector1_50, 0.25f).setEaseOutExpo();
            yield return new WaitForSeconds(0.25f);
            judgementObject.Deactivate();
            gameObject.SetActive(false);
            yield return null;
        }
        private IEnumerator PlayMissTweenCoroutine()
        {
            CancelTween();
            objectProperties.gameObject.SetActive(false);
            judgementObject.Activate();
            LeanTween.alphaCanvas(canvasGroup, 0f, 0.25f).setEaseOutExpo();
            LeanTween.moveLocalY(gameObject, (cTransform.localPosition.y - 50f), 0.25f).setEaseOutExpo();
            LeanTween.rotateLocal(gameObject, missRotateTo, 0.25f);
            yield return new WaitForSeconds(0.25f);
            judgementObject.Deactivate();
            this.gameObject.SetActive(false);
            yield return null;
        }
        private void Awake() => SetMissRotation();
        private void OnEnable()
        {
            ResetProperties();
            PlayApproachTween();
        }
        private void OnDisable() => CancelTween();
    }
}
