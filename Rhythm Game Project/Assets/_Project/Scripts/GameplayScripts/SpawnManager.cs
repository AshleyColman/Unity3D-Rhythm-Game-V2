namespace GameplayScripts
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class SpawnManager : MonoBehaviour
    {
        [SerializeField] private BeatmapController beatmapController = default;
        [SerializeField] private Transform spawn = default;
        [SerializeField] private HitObject prefab = default;
        private List<HitObject> pool = new List<HitObject>();
        private int objectIndex = 0;

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
            pool[objectIndex].SetObjectProperties(beatmapController.Beatmap.PositionArr[objectIndex],
                Color.blue, (objectIndex + 1).ToString());
            pool[objectIndex].gameObject.SetActive(true);
        }
        private void IncrementObjectIndex() => objectIndex++;
    }
}
