namespace AllMenuScripts
{
    using AudioScripts;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class SongSlider : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager = default;
        [SerializeField] private Slider slider = default;
        private float percentage = 0f;

        private void Update()
        {
            if (audioManager.SongAudioSource.isPlaying == true)
            {
                if (audioManager.SongAudioSource.clip is null)
                {
                    percentage = 0f;
                }
                else
                {
                    percentage = (audioManager.SongAudioSource.time / audioManager.SongAudioSource.clip.length) * 100;
                }
                slider.value = percentage;
            }
        }
    }
}