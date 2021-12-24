namespace GameplayScripts
{
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;

    public sealed class GameplayStartPrompt : MonoBehaviour
    {
        public const float DeactivateDuration = 0.25f;
        [SerializeField] private EffectText promptText = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private Transform promptTransform = default;
        [SerializeField] private SoundEffectManager soundEffectManager = default;

        public void Deactivate() => StartCoroutine(DeactivateCoroutine());
        private IEnumerator DeactivateCoroutine()
        {
            soundEffectManager.PlayEffect(soundEffectManager.select1Clip);
            LeanTween.scale(promptText.gameObject, Vector3.zero, 0.2f);
            LeanTween.alphaCanvas(canvasGroup, 0f, 0.25f).setEaseOutExpo();
            LeanTween.moveLocalY(promptTransform.gameObject, (promptTransform.localPosition.y - 50f), 0.25f).setEaseOutExpo();
            LeanTween.rotateLocal(promptTransform.gameObject, new Vector3(promptTransform.localRotation.x,
                promptTransform.localRotation.y, promptTransform.localRotation.z - 45f), 0.25f);
            yield return new WaitForSeconds(DeactivateDuration);
            promptTransform.gameObject.SetActive(false);
            yield return null;
        }
    }
}