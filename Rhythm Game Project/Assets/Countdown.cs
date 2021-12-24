namespace GameplayScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;

    public sealed class Countdown : MonoBehaviour
    {
        [SerializeField] private EffectText text = default;
        [SerializeField] private SoundEffectManager soundEffectManager = default;
        [SerializeField] private ColorOverlay colorOverlay = default;
        private IEnumerator playCountdownCoroutine;

        public void PlayCountdown(int _seconds)
        {
            if (playCountdownCoroutine != null)
            {
                StopCoroutine(playCountdownCoroutine);
            }
            playCountdownCoroutine = PlayCountdownCoroutine(_seconds);
            StartCoroutine(playCountdownCoroutine);
        }
        private IEnumerator PlayCountdownCoroutine(int _seconds)
        {
            WaitForSeconds wait = new WaitForSeconds(1f);
            text.gameObject.SetActive(true);
            for (int i = _seconds; i > 0; i--)
            {
                text.SetText($"{i}");
                text.PlaySetEasePunchTween();
                soundEffectManager.PlayEffect(soundEffectManager.bassClip);
                colorOverlay.PlayOverlayTween(Colors.Pink);
                yield return wait;
            }
            text.SetText("GO");
            text.PlaySetEasePunchTween();
            colorOverlay.PlayOverlayTween(Colors.Yellow);
            soundEffectManager.PlayEffect(soundEffectManager.bassClip);
            yield return wait;
            text.gameObject.SetActive(false);
            yield return null;
        }
    }
}