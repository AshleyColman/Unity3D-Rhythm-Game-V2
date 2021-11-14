namespace BeatmapMenuScripts
{
    using StaticDataScripts;
    using System;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class LeaderboardButton : MonoBehaviour
    {
        [SerializeField] private GameObject playerData = default;
        [SerializeField] private EffectText rankText = default;
        [SerializeField] private EffectText positionText = default;
        [SerializeField] private TextMeshProUGUI playerNameText = default;
        [SerializeField] private TextMeshProUGUI scoreText = default;
        [SerializeField] private TextMeshProUGUI accuracyText = default;
        [SerializeField] private TextMeshProUGUI comboText = default;
        [SerializeField] private TextMeshProUGUI noRecordText = default;
        [SerializeField] private FlashEffect flashEffect = default;
        [field: SerializeField] public Image playerImage { get; private set; }

        public void SetButtonNoData()
        {
            if (playerData.gameObject.activeSelf == true)
            {
                playerData.gameObject.SetActive(false);
                noRecordText.gameObject.SetActive(true);
            }
        }
        public void SetButton(string _username, string _score, string _combo, string _accuracy, UIScripts.Rank _rank)
        {
            if (playerData.gameObject.activeSelf == false)
            {
                playerData.gameObject.SetActive(true);
                noRecordText.gameObject.SetActive(false);
            }
            playerNameText.SetText(_username);
            scoreText.SetText(_score);
            accuracyText.SetText($"{ _accuracy}%");
            comboText.SetText($"{_combo}x");
            rankText.SetText(_rank.Text);
            rankText.SetColorGradient(_rank.ColorGradient);
        }
        public void SetButtonPosition(string _position) => positionText.SetText(_position);
        public void PlayFlashTween()
        {
            if (this.gameObject.activeSelf == true)
            {
                flashEffect.PlayFlashTween();
                PlayTextTween();
            }
        }
        private void OnEnable() => PlayFlashTween();
        private void PlayTextTween()
        {
            rankText.PlaySetEasePunchTween();
            positionText.PlaySetEasePunchTween();
        }
    }
}