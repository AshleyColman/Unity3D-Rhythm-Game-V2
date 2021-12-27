namespace GameplayScripts
{
    using AudioScripts;
    using FileScripts;
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;

    public sealed class BeatmapController : MonoBehaviour
    {
        [SerializeField] private FileManager fileManager = default;
        [SerializeField] private BeatmapLoader loader = default;
        [SerializeField] private SpawnManager spawnManager = default;
        [SerializeField] private Metronome metronome = default;
        [SerializeField] private InputManager inputManager = default;
        [SerializeField] private Countdown countdown = default;
        [SerializeField] private HitObjectController hitObjectController = default;
        [SerializeField] private HitObjectFollower hitObjectFollower = default;
        [SerializeField] private AccuracyManager accuracyManager = default;
        [SerializeField] private ScoreManager scoreManager = default;
        [SerializeField] private GameplayStartPrompt gameplayStartPrompt = default;
        [SerializeField] private AudioManager audioManager = default;
        [SerializeField] private BackgroundManager backgroundManager = default;
        [SerializeField] private FadeTransition fadeTransition = default;
        [SerializeField] private KeyManager keyManager = default;
        private IEnumerator initializeStartCoroutine;
        public Beatmap Beatmap { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsInitialized { get; private set; }
        public void Configure()
        {
            fadeTransition.TransitionIn();
            Beatmap = loader.LoadOsuFile();
            keyManager.DisableKeys(Beatmap.Mode);
            audioManager.LoadAudio(fileManager.GetFirstDirectoryAudioFile($"{Application.persistentDataPath}/Beatmaps/OsuMap"), 0f);
            backgroundManager.LoadBackgroundImage(fileManager.GetFirstDirectoryImageFile($"{Application.persistentDataPath}/Beatmaps/OsuMap"));
            gameplayStartPrompt.LoadBeatmapImage(fileManager.GetFirstDirectoryImageFile($"{Application.persistentDataPath}/Beatmaps/OsuMap"));
            spawnManager.InstantiateHitObjectPool();
            metronome.SetTiming(Beatmap.BeatsPerMinute, Beatmap.OffsetMilliseconds);
            scoreManager.Initialize(Beatmap.ObjectCount);
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
            gameplayStartPrompt.Deactivate();
            yield return new WaitForSeconds((GameplayStartPrompt.DeactivateDuration + 1));
            countdown.PlayCountdown((int)_delay);
            yield return new WaitForSeconds((float)_delay);
            metronome.Play(_delay);
            Run();
            yield return null;
        }
        private void Run()
        {
            IsRunning = true;
            spawnManager.LoopSpawnFromPool();
            hitObjectController.TrackObjects();
            hitObjectFollower.EnableMoveToPosition();
            accuracyManager.TrackIncreasingAccuracy();
        }
        private void Start()
        {
            Configure();
        }
    }
}
