namespace GameplayScripts
{
    using StaticDataScripts;
    using UnityEngine;

    public sealed class Key : MonoBehaviour
    {
        private const string OnKeyDownAnimation = "Key_OnKeyDown";
        private const string OnKeyReleaseAnimation = "Key_OnKeyRelease";

        [SerializeField] private Transform keyTransform = default;
        [SerializeField] private Transform keyTextTransform = default;
        [SerializeField] private Transform keyEffectTextTransform = default;
        [SerializeField] private Animator keyAnimator = default;

        [field: SerializeField] public KeyCode KeyCode { get; private set; }

        public void PlayOnKeyAnimation() => keyAnimator.Play(OnKeyDownAnimation);
        public void PlayOnKeyReleaseAnimation() => keyAnimator.Play(OnKeyReleaseAnimation);
        public void PlayOnKeyDownAnimation()
        {
            LeanTween.cancel(keyTransform.gameObject);
            keyTransform.localScale = Vector3.one;
            LeanTween.cancel(keyTextTransform.gameObject);
            keyTextTransform.localScale = Vector3.one;
            LeanTween.cancel(keyEffectTextTransform.gameObject);
            keyEffectTextTransform.localScale = Vector3.one;

            LeanTween.scale(keyTransform.gameObject, VectorValues.Vector1_25, 0.5f).setEasePunch();
            LeanTween.scale(keyTextTransform.gameObject, VectorValues.Vector1_25, 0.5f).setEasePunch();
            LeanTween.scale(keyEffectTextTransform.gameObject, VectorValues.Vector1_75, 0.5f).setEasePunch();
        }
    }
}
