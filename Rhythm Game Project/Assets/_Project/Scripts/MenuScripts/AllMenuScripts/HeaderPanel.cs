namespace AllMenuScripts
{
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class HeaderPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerButtonText = default;
        [SerializeField] private TextTyper textTyper = default;
        [SerializeField] private TextPanel textPanel = default;
        [SerializeField] private Button backButton = default;

        public void SetPlayerButton(string _text) => playerButtonText.SetText(_text);
        public void TypeText(string _text) => textPanel.TypeText(_text);
        public void EnableBackButton() => backButton.interactable = true;
        public void DisableBackButton() => backButton.interactable = false;
    }
}