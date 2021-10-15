namespace AllMenuScripts
{
    using System.Collections;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class InformationPanel : MonoBehaviour
    {
        private readonly string[] informationArr = new string[]
        {
            "welcome to the game",
            "Developed by Ashleyc97 2021",
            "thank you for having a look :)",
            "why not join the discord?",
            "report bugs to Ashley#3286"
        };
        [SerializeField] private GameObject informationPanel = default;
        [SerializeField] private TextMeshProUGUI mainText = default;
        [SerializeField] private Image colorImage = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private FlashEffect flashEffect = default;
        private Vector3 endPosition;
        private Vector3 startPosition;
        private Transform mainTextTransform;
        private IEnumerator playInfoLoopCoroutine;

        private void OnEnable() => PlayInfoLoop();

        public void PlayInfoLoop()
        {
            informationPanel.gameObject.SetActive(true);
            if (playInfoLoopCoroutine != null)
            {
                StopCoroutine(playInfoLoopCoroutine);
            }
            playInfoLoopCoroutine = PlayInfoLoopCoroutine();
            StartCoroutine(playInfoLoopCoroutine);
        }
        private void Awake()
        {
            mainTextTransform = mainText.transform;
            endPosition = mainTextTransform.localPosition;
            startPosition = new Vector3(endPosition.x, endPosition.y - 50f, endPosition.z);
        }
        private IEnumerator PlayInfoLoopCoroutine()
        {
            WaitForSeconds displayDuration = new WaitForSeconds(6f);
            WaitForSeconds hideDuration = new WaitForSeconds(2f);

            foreach (var info in informationArr)
            {
                flashEffect.PlayFlashTween();
                mainText.SetText(info);
                PlayDisplayTween();
                yield return displayDuration;
                PlayHideTween();
                yield return hideDuration;
            }
            PlayInfoLoop();
            yield return null;
        }
        private void PlayDisplayTween()
        {
            LeanTween.cancel(mainText.gameObject);
            LeanTween.cancel(canvasGroup.gameObject);
            mainTextTransform.localPosition = startPosition;
            canvasGroup.alpha = 0f;
            LeanTween.alphaCanvas(canvasGroup, 1f, 2f).setEaseOutExpo();
            LeanTween.moveLocalY(mainText.gameObject, endPosition.y, 1f).setEaseOutExpo();
        }
        private void PlayHideTween() => LeanTween.alphaCanvas(canvasGroup, 0f, 2f).setEaseOutExpo();
    }
}