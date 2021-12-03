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
        [SerializeField] private HitObject currentObject;
        private double windowMilliseconds = 0.050;
        private double missTime = 0;
        private double okayLateStartTime = 0;
        private double greatLateStartTime = 0;
        private double perfectStartTime = 0;
        private double greatEarlyStartTime = 0;
        private double okayEarlyStartTime = 0;
        [SerializeField] private int currentObjectIndex = 0;
        private IEnumerator trackHitobjects;

        public void SetCurrentObject()
        {
            if (spawnManager.AllSpawned == false)
            {
                if (currentObject is null)
                {
                    if (spawnManager.HitObjectIndex > currentObjectIndex)
                    {
                        currentObject = spawnManager.SpawnedList[currentObjectIndex];
                        SetJudgements();
                    }
                }
            }
        }
        public void SetFirstCurrentObject()
        {
            currentObject = spawnManager.SpawnedList[currentObjectIndex];
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
            if (currentObject != null)
            {
                if (audioManager.SongAudioSourceTime >= okayEarlyStartTime &&
                    audioManager.SongAudioSourceTime < missTime)
                {
                    HasHit();
                }
            }
        }
        private IEnumerator TrackObjectsCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                if (currentObject != null)
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
                HasHit();
            }
        }
        private void IncrementCurrentObjectIndex() => currentObjectIndex++;
        private void SetJudgements()
        {
            double hitTime = beatmapController.Beatmap.HitTimeArr[currentObjectIndex];
            missTime = hitTime + (windowMilliseconds * 5);
            Debug.Log(missTime);
            okayLateStartTime = hitTime + (windowMilliseconds * 3);
            greatLateStartTime = hitTime + windowMilliseconds;
            perfectStartTime = hitTime - windowMilliseconds;
            greatEarlyStartTime = hitTime - (windowMilliseconds * 3);
            okayEarlyStartTime = hitTime - (windowMilliseconds * 5);
        }
        private void HasHit()
        {
            currentObject.PlayHitTween();
            SetCurrentObjectNull();
            IncrementCurrentObjectIndex();
            SetCurrentObject();
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
            currentObject.PlayMissTween();
            SetCurrentObjectNull();
            IncrementCurrentObjectIndex();
            SetCurrentObject();
        }
        private void DeactivateCurrentObject() => currentObject.gameObject.SetActive(false);
        private void SetCurrentObjectNull() => currentObject = null;
    }
}