namespace StartMenuScripts
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using TimingScripts;
    using UIScripts;

    public sealed class StartMenuManager : Menu
    {
        [SerializeField] private GameObject startPanel = default;
        [SerializeField] private TitleText titleText = default;
        [SerializeField] private FadeText fadeText = default;
        [SerializeField] private TimeManager timeManager = default;
        [SerializeField] private FadeTransition fadeTransition = default;
        [SerializeField] private BackgroundManager backgroundManager = default;
        [SerializeField] private TextTyper textTyper = default;
        private IEnumerator startGameCoroutine;
        private bool gameStarted = false;

        //private AccountPanel accountPanel;
        //private DescriptionPanel descriptionPanel;
        //private ControlPanel controlPanel;

        protected override IEnumerator TransitionInCoroutine()
        {
            titleText.SetText(string.Empty);
            fadeText.SetText(string.Empty);
            screen.SetActive(true);
            startPanel.SetActive(true);
            fadeTransition.TransitionIn();
            fadeText.PlayAlphaCanvasTweenLoop();
            timeManager.SetTimeManager(191, 1000);
            yield return new WaitForSeconds(1f);
            textTyper.TypeTextCancelFalse("rhythm game", titleText.TextArr);
            yield return new WaitForSeconds(textTyper.TextTypingDuration("rhythm game"));
            textTyper.TypeTextCancelFalse("click anywhere to start", fadeText.TextArr);
            yield return new WaitForSeconds(textTyper.TextTypingDuration("click anywhere to start"));
            CheckInput();
            yield return null;
        }
        protected override IEnumerator TransitionOutCoroutine()
        {
            fadeTransition.TransitionOut();
            yield return new WaitForSeconds(1f);
            screen.gameObject.SetActive(false);
            yield return null;
        }
        protected override IEnumerator CheckInputCoroutine()
        {
            CheckInputToStartGame();
            return base.CheckInputCoroutine();
        }
        private void Awake() => TransitionIn();
        private void CheckInputToStartGame()
        {
            if (gameStarted == false)
            {
                if (Input.anyKeyDown == true)
                {
                    StartGame();
                }
            }
        }
        private void StartGame()
        {
            if (startGameCoroutine != null)
            {
                StopCoroutine(startGameCoroutine);
            }
            startGameCoroutine = StartGameCoroutine();
            StartCoroutine(startGameCoroutine);
        }
        private IEnumerator StartGameCoroutine()
        {
            gameStarted = true;
            fadeText.StopAlphaCanvasTween();
            fadeText.PlayAlphaCanvasTween(0.25f, 5);
            fadeText.PlaySetEasePunchTween();
            textTyper.TypeTextCancelFalse("game started", titleText.TextArr);
            textTyper.TypeTextCancelFalse("welcome", fadeText.TextArr);
            yield return new WaitForSeconds(2f);
            //accountPanel.TransitionIn();
            //descriptionPanel.TransitionIn();
            //controlPanel.TransitionIn();
        }
    }
}
