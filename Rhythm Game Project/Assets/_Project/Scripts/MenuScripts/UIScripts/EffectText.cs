namespace StartMenuScripts
{
    using StaticDataScripts;
    using TMPro;
    using UnityEngine;

    public class EffectText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI mainText = default;
        [SerializeField] private TextMeshProUGUI effectText = default;
        private Transform mainTextTransform;
        private Transform effectTextTransform;

        public TextMeshProUGUI[] TextArr { get => new TextMeshProUGUI[] { mainText, effectText }; }

        public void PlaySetEasePunchTween()
        {
            CancelTween();
            SetLocalScale();
            LeanTween.scale(mainText.gameObject, VectorValues.Vector1_25, 1f).setEasePunch();
            LeanTween.scale(effectText.gameObject, VectorValues.Vector1_75, 1f).setEasePunch();
        }

        public void SetText(string _text)
        {
            mainText.SetText(_text); effectText.SetText(_text);
        }

        private void Awake()
        {
            mainTextTransform = mainText.transform;
            effectTextTransform = effectText.transform;
        }

        private void CancelTween()
        {
            LeanTween.cancel(mainText.gameObject);
            LeanTween.cancel(effectText.gameObject);
        }

        private void SetLocalScale()
        {
            mainTextTransform.localScale = Vector3.one;
            effectTextTransform.localScale = Vector3.one;
        }
    }
}