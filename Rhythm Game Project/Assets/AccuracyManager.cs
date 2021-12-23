namespace GameplayScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using TMPro;
    using UIScripts;
    using UnityEngine;

    public sealed class AccuracyManager : MonoBehaviour
    {
        private const float lerpDuration = 0.25f;
        [SerializeField] private TextMeshProUGUI accuracyText = default;
        [SerializeField] private ScoreManager scoreManager = default;
        [SerializeField] private RankData rankData = default;
        [SerializeField] private RankSlider rankSlider = default;
        [SerializeField] private BeatmapController beatmapController = default;
        private float totalAccuracy = 0;
        private float lerpTimer = 0f;
        private StringBuilder stringBuilder = new StringBuilder();
        private IEnumerator trackIncreasingAccuracyCoroutine;

        public Rank CurrentRank { get; private set; }
        public float CurrentAccuracy { get; private set; } = 0f;
        private void Awake() => CurrentRank = rankData.GetRank(0f);
        public void UpdateAccuracy()
        {
            totalAccuracy = ((float)scoreManager.TotalBaseScore / (float)scoreManager.MaxBaseScorePossible) * 100;
            ResetAccuracyLerpTimer();
        }
        private void UpdateAccuracyText()
        {
            stringBuilder.Append(CurrentAccuracy.ToString("F2"));
            stringBuilder.Append("%");
            accuracyText.SetText(stringBuilder.ToString());
            stringBuilder.Clear();
        }
        private void CheckIfNewRankAchieved()
        {
            Rank result = rankData.GetRank(CurrentAccuracy);
            if (CurrentRank != result)
            {
                CurrentRank = result;
                rankSlider.SetVisual(CurrentRank);
            }
        }
        public void TrackIncreasingAccuracy()
        {
            if (trackIncreasingAccuracyCoroutine != null)
            {
                StopCoroutine(trackIncreasingAccuracyCoroutine);
            }
            trackIncreasingAccuracyCoroutine = TrackIncreasingAccuracyCoroutine();
            StartCoroutine(trackIncreasingAccuracyCoroutine);
        }
        private void ResetAccuracyLerpTimer() => lerpTimer = 0f;
        private IEnumerator TrackIncreasingAccuracyCoroutine()
        {
            while (beatmapController.IsRunning == true)
            {
                if (CurrentAccuracy < totalAccuracy)
                {
                    lerpTimer += Time.deltaTime / lerpDuration;
                    CurrentAccuracy = Mathf.Lerp(CurrentAccuracy, totalAccuracy, lerpTimer);
                    UpdateAccuracyText();
                    CheckIfNewRankAchieved();
                    rankSlider.UpdateSlider();
                }
                yield return null;
            }
            yield return null;
        }
    }
}