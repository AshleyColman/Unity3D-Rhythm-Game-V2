namespace GameplayScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UIScripts;
    using UnityEngine;

    public sealed class ComboManager : MonoBehaviour
    {
        private const byte ComboBreak = 5;
        private int combo = 0;
        private int highestCombo = 0;
        [SerializeField] private EffectText comboText = default;
        [SerializeField] private MissOverlay missOverlay = default;
        [SerializeField] private SoundEffectManager soundEffectManager = default;
        private IEnumerator playComboBreakTween;

        public void ResetCombo()
        {
            CheckIfHighestCombo();
            CheckIfComboBreak();
            combo = 0;
            UpdateComboText();
        }
        public void IncreaseCombo()
        {
            combo++;
            CheckIfHighestCombo();
            UpdateComboText();
            PlayComboIncreaseTween();
        }
        private void CheckIfComboBreak()
        {
            if (combo >= ComboBreak)
            {
                PlayComboBreakTween();
                soundEffectManager.PlayEffect(soundEffectManager.missClip);
            }
            else
            {
                PlayComboResetTween();
            }
        }
        private void CheckIfHighestCombo()
        {
            if (combo > highestCombo)
            {
                highestCombo = combo;
            }
        }
        private void PlayComboBreakTween()
        {
            if (playComboBreakTween != null)
            {
                StopCoroutine(playComboBreakTween);
            }
            playComboBreakTween = PlayComboBreakTweenCoroutine();
            StartCoroutine(playComboBreakTween);
        }
        private IEnumerator PlayComboBreakTweenCoroutine()
        {
            comboText.PlaySetEasePunchTween();
            comboText.SetColor(Colors.Red);
            missOverlay.PlayOverlayTween();
            yield return new WaitForSeconds(0.25f);
            comboText.SetColor(Colors.White);
            yield return null;
        }
        private void PlayComboIncreaseTween() => comboText.PlaySetEasePunchTween();
        private void PlayComboResetTween()
        {
            comboText.SetColor(Colors.White);
        }
        private void UpdateComboText()
        {
            comboText.SetText($"{combo}x");
        }
    }
}