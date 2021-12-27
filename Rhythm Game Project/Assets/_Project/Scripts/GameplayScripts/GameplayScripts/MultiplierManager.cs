namespace GameplayScripts
{
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UIScripts;
    using UnityEngine;

    public sealed class MultiplierManager : MonoBehaviour
    {
        private const int DefaultMultiplier = 1;
        private const int Bonus = 1;
        [SerializeField] private GameObject multiplierContainer = default;
        [SerializeField] private EffectText multiplierText = default;
        [SerializeField] private TMP_ColorGradient gradientX1 = default;
        [SerializeField] private TMP_ColorGradient gradientX2 = default;
        [SerializeField] private TMP_ColorGradient gradientX3 = default;
        [SerializeField] private TMP_ColorGradient gradientX4 = default;
        [SerializeField] private TMP_ColorGradient gradientX5Plus = default;
        private IEnumerator playDeactivateTweenCoroutine;
        private int highestMultiplier = 0;

        public int Multiplier { get; private set; } = DefaultMultiplier;
        public void ApplyBonusMultiplier()
        {
            Multiplier += Bonus;
            UpdateText();
        }
        public void ResetMultiplier()
        {
            Multiplier = DefaultMultiplier;
            UpdateText();
        }
        public void IncrementMultiplier()
        {
            Multiplier++;
            UpdateText();
        }
        private void CheckIfHighest()
        {
            if (Multiplier > highestMultiplier)
            {
                highestMultiplier = Multiplier;
            }
        }
        private void SetGradient()
        {
            TMP_ColorGradient gradient = Multiplier switch
            {
                1 => gradientX1,
                2 => gradientX2,
                3 => gradientX3,
                4 => gradientX4,
                _ => gradientX5Plus
            };
            multiplierText.SetColorGradient(gradient);
        }
        private void UpdateText()
        {
            multiplierText.SetText($"x{Multiplier}");
            multiplierText.PlaySetEasePunchTween();
            SetGradient();
        }
    }
}