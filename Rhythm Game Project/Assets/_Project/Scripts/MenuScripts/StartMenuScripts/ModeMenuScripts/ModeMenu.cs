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
        [SerializeField] private FadeText descriptionText = default;
        [SerializeField] private Button playButton = default;
        [SerializeField] private BackgroundManager backgroundManager = default;
        [SerializeField] private FlashEffect flashEffect = default;
        [SerializeField] private HeaderPanel headerPanel = default;
        [SerializeField] private Player player = default;
        [SerializeField] private InputPanel inputPanel = default;
        private IEnumerator modeOnPointerEnterCoroutine;
        private readonly string[] modeTextArr = new string[] { "play", "editor", "ranking", "discord", "exit" };
        private readonly string[] descriptionTextArr = new string[] 
        { "enjoy the \nrhythm game",
            "create your \nown map",
            "view online \nrankings",
            "join the \ncommunity",
            "thank you \nfor playing" };
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
            fadeTransition.TransitionIn();
            playButton.Select();
            descriptionText.PlayAlphaCanvasTweenLoop();
            headerPanel.TypeText($"welcome {player.Username}! select a mode");
            yield return null;
        }
        private IEnumerator ModeOnPointerEnterCoroutine(int _modeIndex)
        {
            flashEffect.PlayFlashTween();
            yield return new WaitForSeconds(0.2f);
            textTyper.TypeTextCancelFalse(modeTextArr[_modeIndex], modeText.TextArr);
            textTyper.TypeTextCancelFalse(descriptionTextArr[_modeIndex], descriptionText.TextArr);
            //backgroundManager.TransitionNextImage();
            yield return null;
        }
        private void SetInputPanelControls()
        {
            // TODO 
            inputPanel.SetInputText(new string[] {})
        }
    }
}