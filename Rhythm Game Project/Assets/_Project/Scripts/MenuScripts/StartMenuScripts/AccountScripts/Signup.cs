namespace AccountScripts
{
    using DatabaseScripts;
    using StartMenuScripts;
    using StaticDataScripts;
    using System.Collections;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class Signup : Menu
    {
        [SerializeField] private TMP_InputField usernameInputField = default;
        [SerializeField] private TMP_InputField passwordInputField = default;
        [SerializeField] private TMP_InputField passwordMatchInputField = default;
        [SerializeField] private Image usernameValidationColorImage = default;
        [SerializeField] private Image passwordValidationColorImage = default;
        [SerializeField] private Image passwordValidationMatchColorImage = default;
        [SerializeField] private Button submitButton = default;
        [SerializeField] private Database database = default;
        [SerializeField] private TextPanel textPanel = default;
        [SerializeField] private TitleText titleText = default;
        [SerializeField] private FadeText fadeText = default;
        [SerializeField] private TextTyper textTyper = default;
        [SerializeField] private Colors colors = default;
        private bool isUsernameValid = false;
        private bool isPasswordValid = false;
        private bool isPasswordMatchValid = false;
        private IEnumerator signupCoroutine;
        private IEnumerator transitionInCoroutine;

        public void ValidateUsernameInput()
        {
            if (usernameInputField.text.Length < 4)
            {
                usernameValidationColorImage.color = colors.GetColor(ColorType.red);
                isUsernameValid = false;
            }
            else
            {
                usernameValidationColorImage.color = colors.GetColor(ColorType.lightGreen);
                isUsernameValid = true;
            }
            CheckIfAllInputIsValid();
        }
        public void ValidatePasswordInput()
        {
            if (passwordInputField.text.Length < 4)
            {
                passwordValidationColorImage.color = colors.GetColor(ColorType.red);
                isPasswordValid = false;
            }
            else
            {
                passwordValidationColorImage.color = colors.GetColor(ColorType.lightGreen);
                isPasswordValid = true;
            }
            CheckIfAllInputIsValid();
        }
        public void ValidatePasswordMatchInput()
        {
            if (passwordInputField.text == passwordMatchInputField.text && passwordMatchInputField.text != string.Empty)
            {
                passwordValidationMatchColorImage.color = colors.GetColor(ColorType.lightGreen);
                isPasswordMatchValid = true;
            }
            else
            {
                passwordValidationMatchColorImage.color = colors.GetColor(ColorType.red);
                isPasswordMatchValid = false;
            }
            CheckIfAllInputIsValid();
        }
        public async void Submit_OnClick() => await database.InsertPlayerAsync(usernameInputField.text, passwordInputField.text);
        protected override IEnumerator TransitionInCoroutine()
        {
            screen.gameObject.SetActive(true);
            usernameInputField.Select();
            textTyper.TypeTextCancelFalse("signup", titleText.TextArr);
            yield return new WaitForSeconds(textTyper.TextTypingDuration("signup"));
            textTyper.TypeTextCancelFalse("create a new account", fadeText.TextArr);
            yield return null;
        }
        protected override IEnumerator TransitionOutCoroutine()
        {
            screen.gameObject.SetActive(false);
            yield return null;
        }
        private void CheckIfAllInputIsValid()
        {
            if (isUsernameValid == true && isPasswordValid == true && isPasswordMatchValid == true)
            {
                submitButton.interactable = true;
            }
            else
            {
                submitButton.interactable = false;
            }
        }
    }
}