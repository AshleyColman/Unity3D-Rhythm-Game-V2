namespace GameplayScripts
{
    using UnityEngine;
    using System.Collections;
    using StaticDataScripts;

    public sealed class HitObjectFollower : MonoBehaviour
    {
        private const float TargetTime = 0.25f;
        [SerializeField] private BeatmapController beatmapController = default;
        private Transform followerTransform;
        private float timer = 0f;
        private IEnumerator moveToPositionCoroutine;
        //private HitObjectManager hitobjectManager;

        public void ResetTimer() => timer = 0;
        public void TrackMoveToPosition()
        {
            if (moveToPositionCoroutine != null)
            {
                StopCoroutine(moveToPositionCoroutine);
            }
            moveToPositionCoroutine = MoveToPositionCoroutine();
            StartCoroutine(moveToPositionCoroutine);
        }
        public void PlayRhythmTween()
        {
            LeanTween.cancel(gameObject);
            followerTransform.localScale = Vector3.one;
            LeanTween.scale(gameObject, VectorValues.Vector1_25, 0.1f).setLoopPingPong(1);
        }
        private void Awake() => followerTransform = gameObject.transform;
        private IEnumerator MoveToPositionCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                //if (hitobjectManager.CurrentHittableObject != null && 
                //    hitobjectManager.CurrentHittableObject.gameObject.activeSelf == true)
                //{
                //    timer += Time.deltaTime / TargetTime;
                //    followerTransform.localPosition = Vector3.Lerp(followerTransform.localPosition,
                //      hitobjectManager.CurrentHittableObject.CachedTransform.localPosition, timer);
                //}
                yield return null;
            }
            yield return null;
        }
    }
}
