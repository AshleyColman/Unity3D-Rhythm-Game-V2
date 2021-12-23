namespace GameplayScripts
{
    using StaticDataScripts;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class HitPoint : MonoBehaviour
    {
        [SerializeField] private Transform hitPoint = default;
        [SerializeField] private Image colorImage = default;
        [SerializeField] private ParticleSystem particles = default;
        [SerializeField] private FlashEffect flashEffect = default;

        public void Enable()
        {
            particles.gameObject.SetActive(true);
            colorImage.gameObject.SetActive(true);
            hitPoint.localScale = Vector3.one;
            LeanTween.cancel(hitPoint.gameObject);
            LeanTween.scale(hitPoint.gameObject, VectorValues.Vector1_50, 1f).setEasePunch();
            flashEffect.PlayFlashTween();
        }
        public void Disable()
        {
            particles.gameObject.SetActive(false);
            colorImage.gameObject.SetActive(false);
            flashEffect.PlayFlashTween();
        }
    }
}