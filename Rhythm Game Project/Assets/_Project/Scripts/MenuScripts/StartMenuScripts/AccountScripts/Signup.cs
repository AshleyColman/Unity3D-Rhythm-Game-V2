namespace AccountScripts
{
    using DatabaseScripts;
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class Signup : MonoBehaviour
    {
        private const byte UsernameLength = 4;
        private const byte PasswordLength = 8;
        [SerializeField] private Transform signupPanel = default;
        [SerializeField] private TMP_InputField usernameInputField = default;
        [SerializeField] private TMP_InputField passwordInputField = default;
        [SerializeField] private TMP_InputField passwordConfirmationInputField = default;
        [SerializeField] private Image usernameValidationColorImage = default;
        [SerializeField] private Image passwordValidationColorImage = default;
        [SerializeField] private Image passwordConfirmationValidationColorImage = default;
        [SerializeField] private Button submitButton = default;
        [SerializeField] private Database database = default;
        private IEnumerator signupCoroutine;
        //public void TransitionIn()
        //{
        //    accountPanel.TransitionInPanel(signupPanelTransform);
        //    CheckInput();
        //    textPanel.TransitionInPanel(new Vector3(0f, 580f, 0f), 467.5f);
        //    startMenuManager.SetRhythmAndFadeTextWithTypingAnimation("signup", "create a new account");
        //    usernameInputField.Select();
        //    controlPanel.SetControlText(controlTextArr, keyTextArr);
        //}



        public async void Submit_OnClick() => await database.InsertPlayerAsync(usernameInputField.text, passwordInputField.text);
    }
}