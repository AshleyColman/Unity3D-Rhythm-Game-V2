namespace GameplayScripts
{
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class BeatmapController : MonoBehaviour
    {
        [SerializeField] private BeatmapLoader loader = default;
        [SerializeField] private SpawnManager spawnManager = default;
        [SerializeField] private Metronome metronome = default;
        [SerializeField] private InputManager inputManager = default;
        [SerializeField] private Countdown countdown = default;
        private IEnumerator initializeStartCoroutine;
        public Beatmap Beatmap { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsInitialized { get; private set; }
        public void Configure()
        {
            Beatmap = loader.LoadOsuFile();
            spawnManager.InstantiateHitObjectPool();
            metronome.SetTiming(Beatmap.BeatsPerMinute, Beatmap.OffsetMilliseconds);
            inputManager.CheckToRunGameplay();
        }
        public void InitializeStart(double _delay = 3)
        {
            if (initializeStartCoroutine != null)
            {
                StopCoroutine(initializeStartCoroutine);
            }
            initializeStartCoroutine = InitializeStartCoroutine(_delay);
            StartCoroutine(initializeStartCoroutine);
        }
        private IEnumerator InitializeStartCoroutine(double _delay)
        {
            IsInitialized = true;
            countdown.PlayCountdown((int)_delay);
            yield return new WaitForSeconds((float)_delay);
            metronome.Play(_delay);
            Run();
            yield return null;
        }
        private void Run()
        {
            IsRunning = true;
        }
        private void Start()
        {
            Configure();
        }
    }
}
