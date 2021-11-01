namespace ListMenuScripts
{
    using StaticDataScripts;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class BeatmapButton : MonoBehaviour
    {
        [SerializeField] private Button button = default;
        [SerializeField] private Image modeImage = default;

        public Mode Mode { get; set; }
        [field: SerializeField] public TextMeshProUGUI NumberText { get; private set; } = default;
        [field: SerializeField] public TextMeshProUGUI TitleText { get; private set; } = default;
        [field: SerializeField] public TextMeshProUGUI ArtistText { get; private set; } = default;
        [field: SerializeField] public TextMeshProUGUI CreatorText { get; private set; } = default;
        [field: SerializeField] public TextMeshProUGUI DateText { get; private set; } = default;
        [field: SerializeField] public TextMeshProUGUI ModeText { get; private set; } = default;
        [field: SerializeField] public Image BeatmapImage { get; private set; } = default;

        public void SetButton(string _number, string _title, string _artist, string _creator, string _date, Mode _mode, Color32 _modeColor)
        {
            NumberText.SetText(_number);
            TitleText.SetText(_title);
            ArtistText.SetText(_artist);
            CreatorText.SetText(_creator);
            DateText.SetText(_date);
            ModeText.SetText(ModeData.GetModeString(_mode));
            modeImage.color = _modeColor;
            button.colors = ColorBlockCreator.SetHighlightedColor(button.colors, _modeColor, 64);
            button.colors = ColorBlockCreator.SetSelectedColor(button.colors, _modeColor, 64);
        }
        public void SetButtonNumberText(string _text) => NumberText.text = _text;
        public void Button_OnClick()
        {

        }
        public void Button_OnSelect() => button.Select();
        public void Activate()
        {
            button.transform.SetAsLastSibling();
            button.gameObject.SetActive(true);
        }
        public void Deactivate() => button.gameObject.SetActive(false);

    }
}