using AudioScripts;
using StaticDataScripts;
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
        [SerializeField] private JudgementManager judgementManager = default;
        private double windowMilliseconds = 0.050;
        private double missTime = 0;
        private double okayLateStartTime = 0;
        private double greatLateStartTime = 0;
        private double perfectStartTime = 0;
        private double greatEarlyStartTime = 0;
        private double okayEarlyStartTime = 0;
        private int currentObjectIndex = 0;
        private bool allowPerfectKeyHeldDown = false;
        private IEnumerator trackHitobjects;

        public HitObject CurrentObject { get; private set; }

        public void SetCurrentObject()
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
                    CheckJudgements(audioManager.SongAudioSourceTime);
                }
            }
        }
        private void CheckJudgements(double _time)
        {
            if (_time >= okayEarlyStartTime && _time < greatEarlyStartTime ||
                _time >= okayLateStartTime && _time < missTime)
            {
                HasHit(Judgement.Okay);
            }
            else if (_time >= greatEarlyStartTime && _time < perfectStartTime ||
                _time >= greatLateStartTime && _time < okayLateStartTime)
            {
                HasHit(Judgement.Great);
            }
            else if (_time >= perfectStartTime && _time < greatLateStartTime)
            {
                HasHit(Judgement.Perfect);
            }
        }
        private IEnumerator TrackObjectsCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                if (CurrentObject != null)
                {
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        if (allowPerfectKeyHeldDown == true)
                        {
                            allowPerfectKeyHeldDown = false;
                        }
                        else
                        {
                            allowPerfectKeyHeldDown = true;
                        }
                    }
                    if (allowPerfectKeyHeldDown == true)
                    {
                        if (Input.anyKey)
                        {
                            AutoPlayPerfectHit();
                        }
                        else
                        {
                            CheckMiss();
                        }
                    }
                    else
                    {
                        if (Input.anyKeyDown)
                        {
                            CheckHit();
                        }
                        else
                        {
                            CheckMiss();
                        }
                    }
                }
                yield return null;
            }
            yield return null;
        }
        private void AutoPlayPerfectHit()
        {
            if (audioManager.SongAudioSourceTime >= (perfectStartTime + windowMilliseconds))
            {
                HasHit(Judgement.Perfect);
            }
            else
            {
                CheckMiss();
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
        private void HasHit(Judgement _judgement)
        {
            soundEffectManager.PlayEffect(soundEffectManager.hitClip);
            scoreManager.IncreaseScore(JudgementData.GetJudgementScore(_judgement));
            comboManager.IncreaseCombo();
            judgementManager.IncrementJudgement(_judgement);
            accuracyManager.UpdateAccuracy();
            feverManager.OnHit();
            CurrentObject.SetJudgement(_judgement);
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
            comboManager.ResetCombo();
            CurrentObject.SetJudgement(Judgement.Miss);
            judgementManager.IncrementJudgement(Judgement.Miss);
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