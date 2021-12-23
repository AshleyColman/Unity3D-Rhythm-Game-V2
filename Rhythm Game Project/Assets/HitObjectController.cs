using AudioScripts;
using System.Collections;
using UnityEngine;

namespace GameplayScripts
{
    public sealed class HitObjectController : MonoBehaviour
    {
        [SerializeField] private BeatmapController beatmapController = default;
        [SerializeField] private SpawnManager spawnManager = default;
        [SerializeField] private AudioManager audioManager = default;
        [SerializeField] private SoundEffectManager soundEffectManager = default;
        [SerializeField] private ScoreManager scoreManager = default;
        [SerializeField] private MultiplierManager multiplierManager = default;
        [SerializeField] private ComboManager comboManager = default;
        [SerializeField] private HitObjectFollower hitObjectFollower = default;
        [SerializeField] private FeverManager feverManager = default;
        [SerializeField] private AccuracyManager accuracyManager = default;
        private double windowMilliseconds = 0.050;
        private double missTime = 0;
        private double okayLateStartTime = 0;
        private double greatLateStartTime = 0;
        private double perfectStartTime = 0;
        private double greatEarlyStartTime = 0;
        private double okayEarlyStartTime = 0;
        [SerializeField] private int currentObjectIndex = 0;
        private IEnumerator trackHitobjects;

        public HitObject CurrentObject { get; private set; }

        public void SetCurrentObject()
        {
            if (spawnManager.AllSpawned == false)
            {
                if (CurrentObject is null)
                {
                    if (spawnManager.HitObjectIndex > currentObjectIndex)
                    {
                        CurrentObject = spawnManager.SpawnedList[currentObjectIndex];
                        SetJudgements();
                    }
                }
            }
        }
        public void SetFirstCurrentObject()
        {
            CurrentObject = spawnManager.SpawnedList[currentObjectIndex];
            SetJudgements();
        }
        public void TrackObjects()
        {
            if (trackHitobjects != null)
            {
                StopCoroutine(trackHitobjects);
            }
            trackHitobjects = TrackObjectsCoroutine();
            StartCoroutine(trackHitobjects);
        }
        public void CheckHit()
        {
            if (CurrentObject != null)
            {
                if (audioManager.SongAudioSourceTime >= okayEarlyStartTime &&
                    audioManager.SongAudioSourceTime < missTime)
                {
                    HasHit(JudgementData.PerfectScore);
                }
            }
        }
        private IEnumerator TrackObjectsCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                if (CurrentObject != null)
                {
                    if (Input.anyKey)
                    {
                        AutoPlayHit();
                    }
                    else
                    {
                        CheckMiss();
                    }
                }
                yield return null;
            }
            yield return null;
        }
        private void AutoPlayHit()
        {
            if (audioManager.SongAudioSourceTime >= (perfectStartTime + windowMilliseconds))
            {
                HasHit(JudgementData.PerfectScore);
            }
        }
        private void IncrementCurrentObjectIndex() => currentObjectIndex++;
        private void SetJudgements()
        {
            double hitTime = beatmapController.Beatmap.HitTimeArr[currentObjectIndex];
            missTime = hitTime + (windowMilliseconds * 5);
            okayLateStartTime = hitTime + (windowMilliseconds * 3);
            greatLateStartTime = hitTime + windowMilliseconds;
            perfectStartTime = hitTime - windowMilliseconds;
            greatEarlyStartTime = hitTime - (windowMilliseconds * 3);
            okayEarlyStartTime = hitTime - (windowMilliseconds * 5);
        }
        private void HasHit(int _judgementScore)
        {
            soundEffectManager.PlayHitEffect();
            scoreManager.IncreaseScore(_judgementScore);
            comboManager.IncreaseCombo();
            accuracyManager.UpdateAccuracy();
            feverManager.OnHit();
            CurrentObject.PlayHitTween();
            SetCurrentObjectNull();
            IncrementCurrentObjectIndex();
            SetCurrentObject();
            hitObjectFollower.MoveToNextObject();
        }
        private void CheckMiss()
        {
            if (audioManager.SongAudioSourceTime >= missTime)
            {
                HasMissed();
            }
        }
        private void HasMissed()
        {
            soundEffectManager.PlayMissEffect();
            comboManager.ResetCombo();
            CurrentObject.PlayMissTween();
            SetCurrentObjectNull();
            IncrementCurrentObjectIndex();
            SetCurrentObject();
            hitObjectFollower.MoveToNextObject();
        }
        private void DeactivateCurrentObject() => CurrentObject.gameObject.SetActive(false);
        private void SetCurrentObjectNull() => CurrentObject = null;
    }
}