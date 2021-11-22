namespace GameplayScripts
{
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class JudgementObject : MonoBehaviour
    {
        [SerializeField] private GameObject objectProperties = default;
        [SerializeField] private ParticleSystem particles = default;
        [SerializeField] private Image colorImage = default;
        public void Activate() => gameObject.SetActive(true);
        public void Deactivate() => gameObject.SetActive(false);
        public void SetObjectProperties(Color32 _imageColor, Color _particleColor)
        {
            colorImage.color = _imageColor;
            ParticleSystem.MainModule mainModule = particles.main;
            mainModule.startColor = _particleColor;
        }
    }
}