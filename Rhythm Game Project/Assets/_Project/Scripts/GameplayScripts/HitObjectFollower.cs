namespace GameplayScripts
{
    using UnityEngine;
    using System.Collections;
    using StaticDataScripts;

    public sealed class HitObjectFollower : MonoBehaviour
    {
        private const float TargetTime = 0.25f;
        [SerializeField] private BeatmapController beatmapController = default;
        [SerializeField] private HitObjectController hitObjectController = default;
        private Transform followerTransform;
        private float timer = 0f;
        private IEnumerator moveToPositionCoroutine;

        public void MoveToNextObject() => ResetTimer();
        public void EnableMoveToPosition()
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
        private void ResetTimer() => timer = 0;
        private void Awake() => followerTransform = gameObject.transform;
        private IEnumerator MoveToPositionCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                if (hitObjectController.CurrentObject != null &&
                    hitObjectController.CurrentObject.gameObject.activeSelf == true)
                {
                    timer += Time.deltaTime / TargetTime;
                    followerTransform.localPosition = Vector3.Lerp(followerTransform.localPosition,
                      hitObjectController.CurrentObject.transform.localPosition, timer);
                }
                yield return null;
            }
            yield return null;
        }
    }
}
