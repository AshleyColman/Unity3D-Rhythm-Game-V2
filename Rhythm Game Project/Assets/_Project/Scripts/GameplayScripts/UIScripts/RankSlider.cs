namespace GameplayScripts 
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class RankSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider = default;
        [SerializeField] private TextMeshProUGUI colorImage = default;
        [SerializeField] private FlashEffect flashEffect = default;
        [SerializeField] private EffectText rankText = default;
        [SerializeField] private AccuracyManager accuracyManager;
        [SerializeField] private RankData rankData;

        public void UpdateSlider()
        {
            SetSliderValue();
        }
        public void SetVisual(Rank _rankText)
        {
            rankText.SetColorGradient(_rankText.ColorGradient);
            rankText.SetText(_rankText.Text);
            rankText.PlaySetEasePunchTween();
            colorImage.colorGradientPreset = _rankText.ColorGradient;
            flashEffect.PlayFlashTween();
        }
        private void SetSliderValue()
        {
            if (accuracyManager.CurrentRank == rankData.rankF)
            {
                slider.value = (accuracyManager.CurrentAccuracy / rankData.rankE.ValueToAchieve) * 100;
            }
            else if (accuracyManager.CurrentRank == rankData.rankE)
            {
                slider.value = ((accuracyManager.CurrentAccuracy - rankData.rankE.ValueToAchieve) * 100) /
              (rankData.rankD.ValueToAchieve - rankData.rankE.ValueToAchieve);
            }
            else if (accuracyManager.CurrentRank == rankData.rankD)
            {
                slider.value = ((accuracyManager.CurrentAccuracy - rankData.rankD.ValueToAchieve) * 100) /
              (rankData.rankC.ValueToAchieve - rankData.rankD.ValueToAchieve);
            }
            else if (accuracyManager.CurrentRank == rankData.rankC)
            {
                slider.value = ((accuracyManager.CurrentAccuracy - rankData.rankC.ValueToAchieve) * 100) /
              (rankData.rankB.ValueToAchieve - rankData.rankC.ValueToAchieve);
            }
            else if (accuracyManager.CurrentRank == rankData.rankB)
            {
                slider.value = ((accuracyManager.CurrentAccuracy - rankData.rankB.ValueToAchieve) * 100) /
              (rankData.rankA.ValueToAchieve - rankData.rankB.ValueToAchieve);
            }
            else if (accuracyManager.CurrentRank == rankData.rankA)
            {
                slider.value = ((accuracyManager.CurrentAccuracy - rankData.rankA.ValueToAchieve) * 100) /
              (rankData.rankS.ValueToAchieve - rankData.rankA.ValueToAchieve);
            }
        }
    }
}