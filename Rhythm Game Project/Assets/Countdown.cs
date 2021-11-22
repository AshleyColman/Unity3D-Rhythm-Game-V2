namespace GameplayScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;

    public sealed class Countdown : MonoBehaviour
    {
        [SerializeField] private EffectText text = default;
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
                yield return wait;
            }
            text.SetText("GO");
            yield return wait;
            text.gameObject.SetActive(false);
            yield return null;
        }
    }
}