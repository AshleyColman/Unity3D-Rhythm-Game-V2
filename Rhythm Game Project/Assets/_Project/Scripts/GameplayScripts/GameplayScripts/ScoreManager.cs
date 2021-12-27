namespace GameplayScripts
{
    using System.Collections;
    using System.Text;
    using TMPro;
    using UnityEngine;

    public sealed class ScoreManager : MonoBehaviour
    {
        private const float scoreLerpDuration = 0.25f;
        private int totalScore = 0;
        private int currentScore = 0;
        private int currentBaseScore = 0;
        private float scoreLerpTimer = 0f;
        [SerializeField] private TextMeshProUGUI scoreText = default;
        [SerializeField] private BeatmapController beatmapController = default;
        [SerializeField] private MultiplierManager multiplierManager = default;
        [SerializeField] private ScoreEffect scoreEffect = default;
        private StringBuilder scoreStringBuilder = new StringBuilder();
        private Transform scoreEffectTextTransform;

        public int TotalBaseScore { get; private set; } = 0;
        public int MaxBaseScorePossible { get; private set; } = 0;

        public void Initialize(int _totalObjects) => CalculateMaxBaseScorePossible(_totalObjects);
        public void IncreaseScore(int _score)
        {
            ResetScoreLerpTimer();
            scoreEffect.PlayScoreEffect(_score * multiplierManager.Multiplier);
            totalScore += (_score * multiplierManager.Multiplier);
            TotalBaseScore += _score;
        }
        public void CalculateMaxBaseScorePossible(int _totalObjects) => MaxBaseScorePossible = _totalObjects * JudgementData.PerfectScore;
        private void ResetScoreLerpTimer() => scoreLerpTimer = 0f;
        private void Update()
        {
            if (beatmapController.IsRunning == true)
            {
                if (currentScore != totalScore)
                {
                    scoreLerpTimer += Time.deltaTime / scoreLerpDuration;
                    currentScore = (int)Mathf.Lerp(currentScore, totalScore, scoreLerpTimer);
                    currentBaseScore = (int)Mathf.Lerp(currentBaseScore, TotalBaseScore, scoreLerpTimer);
                    UpdateScoreText();
                }
            }
        }
        private void UpdateScoreText()
        {
            scoreStringBuilder.Append(currentScore);
            scoreStringBuilder = UtilityMethods.AddZerosToScoreString(scoreStringBuilder);
            scoreText.SetText(scoreStringBuilder.ToString());
            scoreStringBuilder.Clear();
        }
    }
}