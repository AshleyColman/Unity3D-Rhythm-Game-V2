namespace UIScripts
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
        private float effectTextOpacity;

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
            mainText.SetText(_text); 
            effectText.SetText(_text);
        }
        public void SetColorGradient(TMP_ColorGradient _colorGradient)
        {
            mainText.colorGradientPreset = _colorGradient;
            effectText.colorGradientPreset = _colorGradient;
        }
        public void SetColor(Color _color)
        {
            mainText.color = _color;
            _color.a = effectTextOpacity;
            effectText.color = _color;
        }
        private void Awake()
        {
            mainTextTransform = mainText.transform;
            effectTextTransform = effectText.transform;
            effectTextOpacity = effectText.color.a;
        }
        private void CancelTween()
        {
            LeanTween.cancel(mainText.gameObject);
            LeanTween.cancel(effectText.gameObject);
        }
        private void SetLocalScale()
        {
            if (mainTextTransform != null && effectTextTransform != null)
            {
                mainTextTransform.localScale = Vector3.one;
                effectTextTransform.localScale = Vector3.one;
            }
        }
    }
}