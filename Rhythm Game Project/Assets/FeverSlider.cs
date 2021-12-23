namespace GameplayScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class FeverSlider : MonoBehaviour
    {
        private const float IncrementAmount = 1f;
        [SerializeField] private Slider slider = default;
        [SerializeField] private Image fill = default;
        [SerializeField] private Gradient gradient = default;
        [SerializeField] private FlashEffect flashEffect = default;
        [SerializeField] private FeverManager feverManager = default;
        private float lerpToValue = 0f;
        private float lerpFromValue = 0f;
        private float lerpValue = 0f;

        public float SliderValue { get { return slider.value; } set { slider.value = value; } }

        public void IncrementSlider()
        {
            if (slider.value < slider.maxValue)
            {
                slider.value += IncrementAmount;
            }
        }
        public void SetSliderLerpValues()
        {
            lerpValue = 0f;
            lerpFromValue = slider.value;
        }
        public void LerpSliderDown(double _feverDuration)
        {
            lerpValue += (float)(Time.deltaTime / _feverDuration);
            slider.value = Mathf.Lerp(lerpFromValue, lerpToValue, lerpValue);
        }
        public void PlayFlash() => flashEffect.PlayFlashTween();
        public void SetColor() => fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
