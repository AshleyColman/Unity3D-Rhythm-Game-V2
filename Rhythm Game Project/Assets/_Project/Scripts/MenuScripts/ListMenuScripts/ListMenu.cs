namespace ListMenuScripts
{
    using AllMenuScripts;
    using System.Collections;
    using TMPro;
    using UIScripts;
    using UnityEngine;

    public class ListMenu : Menu
    {
        [SerializeField] private TextMeshProUGUI titleText = default;
        [SerializeField] private FadeTransition fadeTransition = default;
        [SerializeField] private TextTyper textTyper = default;
        [SerializeField] private HeaderPanel headerPanel = default;
        [SerializeField] private InputPanel inputPanel = default;
        [SerializeField] private ButtonList buttonList = default;

        protected override IEnumerator TransitionInCoroutine()
        {
            fadeTransition.TransitionIn();
            SetInputPanelControls();
            textTyper.TypeTextCancelFalse("beatmap select");
            headerPanel.TypeText($"select a beatmap to play");
            headerPanel.EnableBackButton();
            LoadButtonList();
            yield return null;
        }
        private void SetInputPanelControls()
        {
            string[] controlArr = new string[] { "navigate:", "select:", "next button:", "previous button:", "go back" };
            string[] keyArr = new string[] { "mouse", "enter", "down", "up", "escape" };
            inputPanel.SetInputText(controlArr, keyArr);
        }
        private void LoadButtonList()
        {
            if (buttonList.HasInstantiatedButtonList == false)
            {
                buttonList.InstantiateNewList();
            }
        }
    }
}