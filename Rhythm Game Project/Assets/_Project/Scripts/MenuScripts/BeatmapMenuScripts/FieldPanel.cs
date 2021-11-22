namespace BeatmapMenuScripts
{
    using DatabaseScripts;
    using PlayerScripts;
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;

    public sealed class FieldPanel : MonoBehaviour
    {
        [SerializeField] private FieldButton scoreButton = default;
        [SerializeField] private FieldButton comboButton = default;
        [SerializeField] private FieldButton accuracyButton = default;
        [SerializeField] private FieldButton perfectButton = default;
        [SerializeField] private FieldButton greatButton = default;
        [SerializeField] private FieldButton okayButton = default;
        [SerializeField] private FieldButton missButton = default;
        [SerializeField] private FieldButton feverScoreButton = default;
        [SerializeField] private FieldButton rankButton = default;
        private FieldButton[] buttonArr;
        [SerializeField] private Database database = default;
        [SerializeField] private Player player = default;
        [SerializeField] private RankData rankData = default;
        private IEnumerator setPanelCoroutine;
        private IEnumerator playAnimationTweenCoroutine;

        private void Awake() => SetButtonArr();
        private void SetButtonArr()
        {
            List<FieldButton> buttonList = new List<FieldButton>();
            buttonList.Add(rankButton);
            buttonList.Add(scoreButton);
            buttonList.Add(comboButton);
            buttonList.Add(accuracyButton);
            buttonList.Add(perfectButton);
            buttonList.Add(greatButton);
            buttonList.Add(okayButton);
            buttonList.Add(missButton);
            buttonList.Add(feverScoreButton);
            buttonArr = buttonList.ToArray();
        }
        public void SetPanel()
        {
            StopAllCoroutines();
            setPanelCoroutine = SetPanelCoroutine();
            StartCoroutine(setPanelCoroutine);
        }
        private IEnumerator SetPanelCoroutine()
        {
            SetFieldButtonNull();
            string[] data = database.GetPlayerBeatmapData(player.Username, "beatmap");
            if (data != null && data.Length > 0)
            {
                SetFieldButtonData(data[0], data[1], data[2], rankData.GetRank(data[3]), data[4], data[5], data[6], data[7],
                    data[8]);
            }
            PlayAnimationTween();            
            yield return null;
        }
        private void SetFieldButtonData(string _score, string _combo, string _accuracy, Rank _rank, string _perfect, 
            string _great, string _okay, string _miss, string _feverScore)
        {
            scoreButton.SetValueText(_score);
            comboButton.SetValueText($"{_combo}x");
            accuracyButton.SetValueText($"{_accuracy}%");
            perfectButton.SetValueText(_perfect);
            greatButton.SetValueText(_great);
            okayButton.SetValueText(_okay);
            missButton.SetValueText(_miss);
            feverScoreButton.SetValueText(_feverScore);
            rankButton.SetValueText(_rank.Text);
            rankButton.SetValueTextColorGradient(_rank.ColorGradient);
        }
        private void SetFieldButtonNull()
        {
            string nullString = "-";
            Rank nullRank = rankData.GetRank();
            scoreButton.SetValueText(nullString);
            comboButton.SetValueText(nullString);
            accuracyButton.SetValueText(nullString);
            perfectButton.SetValueText(nullString);
            greatButton.SetValueText(nullString);
            okayButton.SetValueText(nullString);
            missButton.SetValueText(nullString);
            feverScoreButton.SetValueText(nullString);
            rankButton.SetValueText(nullRank.Text);
            rankButton.SetValueTextColorGradient(nullRank.ColorGradient);
        }
        private void PlayAnimationTween()
        {
            if (playAnimationTweenCoroutine != null)
            {
                StopCoroutine(playAnimationTweenCoroutine);
            }
            playAnimationTweenCoroutine = PlayAnimationTweenCoroutine();
            StartCoroutine(playAnimationTweenCoroutine);
        }
        private IEnumerator PlayAnimationTweenCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.1f);
            foreach (var button in buttonArr)
            {
                button.PlayFlashTween();
                button.PlayTextTween();
                yield return wait;
            }
            yield return null;
        }
        private void StopAllFieldPanelCoroutines()
        {
            StopAllCoroutines();
            scoreButton.StopAllCoroutines();
            comboButton.StopAllCoroutines();
            accuracyButton.StopAllCoroutines();
            perfectButton.StopAllCoroutines();
            greatButton.StopAllCoroutines();
            okayButton.StopAllCoroutines();
            missButton.StopAllCoroutines();
            feverScoreButton.StopAllCoroutines();
            rankButton.StopAllCoroutines();
        }
    }
}
