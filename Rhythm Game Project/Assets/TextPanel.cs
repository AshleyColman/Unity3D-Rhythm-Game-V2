namespace UIScripts
{
    using TMPro;
    using UnityEngine;

    public sealed class TextPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI mainText = default;
        [SerializeField] private TextTyper textTyper = default;
        private Transform mainTextTransform;

        public void TypeText(string _text) => textTyper.TypeTextCancelTrue(_text, mainText);
        private void Awake() => mainText.SetText(string.Empty);
    }
}