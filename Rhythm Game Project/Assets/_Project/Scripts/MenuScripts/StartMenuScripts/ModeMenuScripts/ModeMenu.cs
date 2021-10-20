namespace ModeMenuScripts
{
    using AllMenuScripts;
    using PlayerScripts;
    using System.Collections;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class ModeMenu : Menu
    {
        [SerializeField] private FadeTransition fadeTransition = default;
        [SerializeField] private TextTyper textTyper = default;
        [SerializeField] private EffectText modeText = default;
        [SerializeField] private Button playButton = default;
        [SerializeField] private ModeMenuBackgroundManager backgroundManager = default;
        [SerializeField] private FlashEffect flashEffect = default;
        [SerializeField] private HeaderPanel headerPanel = default;
        [SerializeField] private Player player = default;
        [SerializeField] private InputPanel inputPanel = default;
        [SerializeField] private InformationPanel informationPanel = default;
        [SerializeField] private Color32[] modeColorArr = default;
        private IEnumerator modeOnPointerEnterCoroutine;
        private readonly string[] modeTextArr = new string[] { "play", "editor", "ranking", "discord", "exit" };
        private readonly string[] modeDescriptionTextArr = new string[] 
        { "play on beatmaps created by other players", 
            "create your own beatmap using the editor", 
            "view global rankings", 
            "join the community", 
            "thank you for playing" };
        
        public void Button_OnPointerEnter(int _modeIndex)
        {
            if (modeOnPointerEnterCoroutine != null)
            {
                StopCoroutine(modeOnPointerEnterCoroutine);
            }
            modeOnPointerEnterCoroutine = ModeOnPointerEnterCoroutine(_modeIndex);
            StartCoroutine(modeOnPointerEnterCoroutine);
        }
        protected override IEnumerator TransitionInCoroutine()
        {
            SetInputPanelControls();
            playButton.Select();
            headerPanel.TypeText($"welcome {player.Username}! select a mode");
            fadeTransition.TransitionIn();
            yield return null;
        }
        private IEnumerator ModeOnPointerEnterCoroutine(int _modeIndex)
        {
            modeText.SetText(string.Empty);
            backgroundManager.TransitionToIndex(_modeIndex);
            flashEffect.PlayFlashInTween();
            textTyper.TypeTextCancelFalse(modeTextArr[_modeIndex], modeText.TextArr);
            informationPanel.PlaySingleInfo(modeDescriptionTextArr[_modeIndex], modeColorArr[_modeIndex]);
            yield return null;
        }
        private void SetInputPanelControls()
        {
            string[] controlArr = new string[] { "navigate:", "select:", "next button:", "previous button:" };
            string[] keyArr = new string[] { "mouse", "enter", "down", "up"};
            inputPanel.SetInputText(controlArr, keyArr);
        }
    }
}