namespace ListMenuScripts
{
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class BeatmapButton : MonoBehaviour
    {
        [SerializeField] private Button button = default;
        [SerializeField] private TextMeshProUGUI numberText = default;
        [SerializeField] private TextMeshProUGUI titleText = default;
        [SerializeField] private TextMeshProUGUI artistText = default;
        [SerializeField] private TextMeshProUGUI creatorText = default;
        [SerializeField] private TextMeshProUGUI dateText = default;
        [SerializeField] private TextMeshProUGUI modeText = default;
        [SerializeField] private Image modeImage = default;

        [field: SerializeField] public Image BeatmapImage { get; private set; }

        public void SetButton(string _number, string _title, string _artist, string _creator, string _date, string _mode, Color32 _modeColor)
        {
            numberText.SetText(_number);
            titleText.SetText(_title);
            artistText.SetText(_artist);
            creatorText.SetText(_creator);
            dateText.SetText(_date);
            modeText.SetText(_mode);
            modeImage.color = _modeColor;
            button.colors = ColorBlockCreator.SetHighlightedColor(button.colors, _modeColor, 64);
        }
        public void Button_OnClick()
        {

        }
    }
}