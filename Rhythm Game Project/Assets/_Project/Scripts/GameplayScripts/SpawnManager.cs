namespace GameplayScripts
{
    using AudioScripts;
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;

    public sealed class SpawnManager : MonoBehaviour
    {
        [SerializeField] private BeatmapController beatmapController = default;
        [SerializeField] private AudioManager audioManager = default;
        [SerializeField] private HitObjectController hitObjectController = default;
        [SerializeField] private Transform spawn = default;
        [SerializeField] private HitObject prefab = default;
        [SerializeField] private List<HitObject> pool = new List<HitObject>();
        private int poolObjectIndex = 0;
        private IEnumerator loopSpawnFromPool;

        [field: SerializeField] public List<HitObject> SpawnedList { get; private set; } = new List<HitObject>();
        [field: SerializeField] public bool AllSpawned { get; private set; }
        [field: SerializeField] public int HitObjectIndex { get; private set; } = 0;

        public void LoopSpawnFromPool()
        {
            if (loopSpawnFromPool != null)
            {
                StopCoroutine(loopSpawnFromPool);
            }
            loopSpawnFromPool = LoopSpawnFromPoolCoroutine();
            StartCoroutine(loopSpawnFromPool);
        }
        private IEnumerator LoopSpawnFromPoolCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                CheckAllSpawned();
                if (HitObjectIndex < beatmapController.Beatmap.ObjectCount)
                {
                    if (audioManager.SongAudioSourceTime >= beatmapController.Beatmap.SpawnTimeArr[HitObjectIndex])
                    {
                        SpawnFromPool();
                    }
                }
                yield return null;
            }
            yield return null;
        }
        public void InstantiateHitObjectPool(int _poolSize = 50)
        {
            for (int i = 0; i < _poolSize; i++)
            {
                HitObject hitObject = Instantiate(prefab, spawn);
                pool.Add(hitObject);
            }
        }
        private void SpawnFromPool()
        {
            CheckResetPoolIndex();
            
            pool[poolObjectIndex].SetObjectProperties(
                (float)ApproachRateData.GetApproachRate(beatmapController.Beatmap.ApproachRate),
                beatmapController.Beatmap.PositionArr[HitObjectIndex],
                Colors.ColorArr[Random.Range(0,6)],
                (HitObjectIndex + 1).ToString());

            pool[poolObjectIndex].gameObject.SetActive(true);

            SpawnedList.Add(pool[poolObjectIndex]);
            SetObjectToTrack();
            poolObjectIndex++;
            HitObjectIndex++;
        }
        private void CheckResetPoolIndex()
        {
            if (poolObjectIndex >= pool.Count)
            {
                poolObjectIndex = 0;
            }
        }
        private void SetObjectToTrack()
        {
            if (HitObjectIndex == 0)
            {
                hitObjectController.SetFirstCurrentObject();
            }
            else
            {
                hitObjectController.SetCurrentObject();
            }
        }
        private void CheckAllSpawned()
        {
            if (HitObjectIndex >= beatmapController.Beatmap.ObjectCount)
            {
                AllSpawned = true;
            }
        }
    }
}