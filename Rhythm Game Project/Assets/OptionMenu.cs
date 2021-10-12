namespace AccountScripts
{
    using System.Collections;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class OptionMenu : Menu
    {
        [SerializeField] private Button signupButton = default;
        [SerializeField] private Button loginButton = default;
        [SerializeField] private Button offlineButton = default;
        [SerializeField] private TextPanel textPanel = default;

        public void SignupButton_OnSelect() => textPanel.TypeText("create a new account");
        public void LoginButton_OnSelect() => textPanel.TypeText("login to your account");
        public void OfflineButton_OnSelect() => textPanel.TypeText("play offline");
        protected override IEnumerator TransitionInCoroutine()
        {
            screen.gameObject.SetActive(true);
            signupButton.Select();
            return base.TransitionInCoroutine();
        }
    }
}