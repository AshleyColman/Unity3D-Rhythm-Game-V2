namespace GameplayScripts
{
    using AudioScripts;
    using System.Collections;
    using System.Text;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class FeverManager : MonoBehaviour
    {
        private readonly int[] HitPointTargetArr = new int[] { 25, 50, 75, 100 };
        [SerializeField] private MultiplierManager multiplierManager = default;
        [SerializeField] private HitPoint[] HitPointArr = default;
        [SerializeField] private FeverBackground feverBackground = default;
        [SerializeField] private FeverSlider feverSlider = default;
        [SerializeField] private AudioManager audioManager = default;
        [SerializeField] private SoundEffectManager soundEffectManager = default;
        [SerializeField] private ParticleSystem particles = default;
        private double tickDuration = 0.34;
        private double measureDuration = 0;
        private double feverDuration = 0;
        private double maxFeverDuration = 0;
        private double activatedTimer = 0;
        private int hitPointTargetIndex = 0;
        private IEnumerator trackActivatedTimeCoroutine;

        public bool CanActivate { get; private set; } = false;
        public bool Activated { get; private set; } = false;

        private void Awake()
        {
            CalculateMeasureDuration();
            CalculateMaxFeverDuration();
        }
        public void OnHit()
        {
            if (Activated == false)
            {
                CheckHitPoints();
                feverSlider.IncrementSlider();
                feverSlider.SetColor();
                CalculateFeverDuration();
                CheckIfCanActivate();
            }
        }
        private void CheckIfCanActivate()
        {
            if (CanActivate == false)
            {
                if (feverSlider.SliderValue >= HitPointTargetArr[0])
                {
                    CanActivate = true;
                }
            }
        }
        private void CheckHitPoints()
        {
            if (hitPointTargetIndex < HitPointTargetArr.Length)
            {
                if (feverSlider.SliderValue >= HitPointTargetArr[hitPointTargetIndex])
                {
                    HitPointArr[hitPointTargetIndex].Enable();
                    multiplierManager.IncrementMultiplier();
                    feverSlider.PlayFlash();
                    hitPointTargetIndex++;
                }
            }
        }
        private void CheckHitPointsDuringActivation()
        {
            if (hitPointTargetIndex > -1 && hitPointTargetIndex <= HitPointTargetArr.Length)
            {
                if (feverSlider.SliderValue < HitPointTargetArr[hitPointTargetIndex])
                {
                    HitPointArr[hitPointTargetIndex].Disable();
                    hitPointTargetIndex--;
                }
            }
        }
        public void Activate()
        {
            if (Activated == false && CanActivate == true)
            {
                Activated = true;
                CanActivate = false;
                hitPointTargetIndex--;
                soundEffectManager.PlayEffect(soundEffectManager.select2Clip);
                audioManager.EnableReverbFilter();
                multiplierManager.ApplyBonusMultiplier();
                feverSlider.SetSliderLerpValues();
                feverSlider.PlayFlash();
                feverBackground.PlayAnimation();
                particles.gameObject.SetActive(true);
                TrackActivatedTime();
            }
        }
        private void Deactivate()
        {
            Activated = false;
            CanActivate = false;
            hitPointTargetIndex = 0;
            feverSlider.SliderValue = 0;
            multiplierManager.ResetMultiplier();
            particles.gameObject.SetActive(false);
            feverBackground.StopAnimation();
            audioManager.DisableReverbFilter();
        }
        private void CalculateFeverDuration() => feverDuration = (maxFeverDuration * feverSlider.SliderValue) / 100;
        private void CalculateMaxFeverDuration() => maxFeverDuration = (measureDuration * 8);
        private void CalculateMeasureDuration() => measureDuration = (tickDuration * 4);
        private void TrackActivatedTime()
        {
            if (trackActivatedTimeCoroutine != null)
            {
                StopCoroutine(trackActivatedTimeCoroutine);
            }
            trackActivatedTimeCoroutine = TrackActivatedTimeCoroutine();
            StartCoroutine(trackActivatedTimeCoroutine);
        }
        private IEnumerator TrackActivatedTimeCoroutine()
        {
            activatedTimer = feverDuration;
            while (Activated == true)
            {
                activatedTimer -= Time.deltaTime;
                feverSlider.LerpSliderDown(feverDuration);
                feverSlider.SetColor();
                CheckHitPointsDuringActivation();
                if (activatedTimer <= 0)
                {
                    Deactivate();
                }
                yield return null;
            }
            yield return null;
        }
    }
}