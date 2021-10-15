namespace AllMenuScripts
{
    using TMPro;
    using UIScripts;
    using UnityEngine;

    public sealed class HeaderPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerButtonText = default;
        [SerializeField] private TextTyper textTyper = default;
        [SerializeField] private TextPanel textPanel = default;

        public void SetPlayerButton(string _text) => playerButtonText.SetText(_text);
        public void TypeText(string _text) => textPanel.TypeText(_text);
    }
}