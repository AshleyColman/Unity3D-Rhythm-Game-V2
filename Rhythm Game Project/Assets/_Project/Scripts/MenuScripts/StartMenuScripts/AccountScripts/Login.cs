namespace AccountScripts
{
    using AllMenuScripts;
    using DatabaseScripts;
    using ModeMenuScripts;
    using PlayerScripts;
    using StartMenuScripts;
    using System.Collections;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class Login : Menu
    {
        [SerializeField] private TMP_InputField usernameInputField = default;
        [SerializeField] private TMP_InputField passwordInputField = default;
        [SerializeField] private Button submitButton = default;
        [SerializeField] private Database database = default;
        [SerializeField] private TitleText titleText = default;
        [SerializeField] private FadeText fadeText = default;
        [SerializeField] private TextTyper textTyper = default;
        [SerializeField] private Player player = default;
        [SerializeField] private StartMenuManager startMenuManager = default;

        public async void Submit_OnClick()
        {
            bool loggedIn = await database.LoginPlayerAsync(usernameInputField.text, passwordInputField.text);
            if (loggedIn == true)
            {
                player.LoggedIn = true;
                player.Username = usernameInputField.text;
                startMenuManager.TransitionOut();
            }
            else
            {
                usernameInputField.text = string.Empty;
                passwordInputField.text = string.Empty;
                usernameInputField.Select();
            }
        }

        public void ValidateInputfields()
        {
            if (usernameInputField.text.Length > 0 && passwordInputField.text.Length > 0)
            {
                submitButton.interactable = true;
            }
            else
            {
                submitButton.interactable = false;
            }
        }
        protected override IEnumerator TransitionInCoroutine()
        {
            screen.gameObject.SetActive(true);
            usernameInputField.Select();
            textTyper.TypeTextCancelFalse("login", titleText.TextArr);
            yield return new WaitForSeconds(textTyper.TextTypingDuration("login"));
            textTyper.TypeTextCancelFalse("login to your account", fadeText.TextArr);
            yield return null;
        }
        protected override IEnumerator TransitionOutCoroutine()
        {
            screen.gameObject.SetActive(false);
            yield return null;
        }
    }
}
