namespace GameplayScripts
{
    using UIScripts;
    using UnityEngine;

    public sealed class FeverBackground : MonoBehaviour
    {
        private const string AnimationKey = "FeverBackground_Activated";
        [SerializeField] private Animator animator = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private FlashEffect flashEffect = default;

        private void PlayFadeIn()
        {
            LeanTween.cancel(canvasGroup.gameObject);
            canvasGroup.alpha = 0f;
            LeanTween.alphaCanvas(canvasGroup, 0.1f, 0.5f).setEaseOutExpo();
        }
        private void PlayFadeOut()
        {
            LeanTween.cancel(canvasGroup.gameObject);
            canvasGroup.alpha = 0.1f;
            LeanTween.alphaCanvas(canvasGroup, 0f, 0.5f).setEaseOutExpo();
        }
        public void PlayAnimation()
        {
            flashEffect.PlayFlashTween();
            PlayFadeIn();
            animator.speed = 1;
            animator.Play(AnimationKey);
        }
        public void StopAnimation() 
        {
            PlayFadeOut();
            animator.speed = 0; 
        }
    }
}