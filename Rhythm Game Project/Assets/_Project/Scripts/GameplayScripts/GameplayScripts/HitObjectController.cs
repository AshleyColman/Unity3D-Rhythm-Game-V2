using AudioScripts;
using StaticDataScripts;
using System.Collections;
using TMPro;
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
        [SerializeField] private double windowMilliseconds = 0.050;
        [SerializeField] private double missTime = 0;
        [SerializeField] private double okayLateStartTime = 0;
        [SerializeField] private double greatLateStartTime = 0;
        [SerializeField] private double perfectStartTime = 0;
        [SerializeField] private double greatEarlyStartTime = 0;
        [SerializeField] private double okayEarlyStartTime = 0;
        [SerializeField] private int currentObjectIndex = 0;
        [SerializeField] private double time = 0; // Testing
        [SerializeField] private bool allowPerfectKeyHeldDown = false;
        [SerializeField] private TextMeshProUGUI autoText = default;
        private IEnumerator trackHitobjects;

        [field: SerializeField] public HitObject CurrentObject { get; private set; }

        public void SetCurrentObject()
        {
            if (CurrentObject is null)
            {
                if (spawnManager.SpawnedList.Count > currentObjectIndex)
                {
                    CurrentObject = spawnManager.SpawnedList[currentObjectIndex];
                    SetJudgements();
                }
                //if (spawnManager.HitObjectIndex > currentObjectIndex)
                //{
                //    print("3");
                //    CurrentObject = spawnManager.SpawnedList[currentObjectIndex];
                //    SetJudgements();
                //}
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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (allowPerfectKeyHeldDown == true)
                {
                    allowPerfectKeyHeldDown = false;
                    autoText.SetText("Auto Off");
                }
                else
                {
                    allowPerfectKeyHeldDown = true;
                    autoText.SetText("Auto On");
                }
            }
        }
        private IEnumerator TrackObjectsCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                time = audioManager.SongAudioSourceTime;
                if (CurrentObject != null)
                {
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