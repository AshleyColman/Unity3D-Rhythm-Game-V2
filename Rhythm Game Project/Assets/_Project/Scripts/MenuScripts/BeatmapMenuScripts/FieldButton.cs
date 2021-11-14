namespace UIScripts
{
    using TMPro;
    using UnityEngine;

    public sealed class FieldButton : MonoBehaviour
    {
        [SerializeField] private EffectText fieldText = default;
        [SerializeField] private EffectText valueText = default;
        [SerializeField] private FlashEffect flashEffect = default;

        public void SetValueText(string _text) => valueText.SetText(_text);
        public void SetValueTextColorGradient(TMP_ColorGradient _colorGradient) => valueText.SetColorGradient(_colorGradient);
        public void PlayFlashTween() => flashEffect.PlayFlashTween();
        public void PlayTextTween()
        {
            fieldText.PlaySetEasePunchTween();
            valueText.PlaySetEasePunchTween();
        }
    }
}