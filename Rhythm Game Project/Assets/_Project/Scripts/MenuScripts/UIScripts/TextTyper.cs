namespace UIScripts
{
    using System.Collections;
    using System.Text;
    using TMPro;
    using UnityEngine;

    public sealed class TextTyper : MonoBehaviour
    {
        private const float DefaultPerLetterDuration = 0.02f;
        private IEnumerator typeTextCancelTrueCoroutine;
        private IEnumerator typeTextCancelFalseCoroutine;

        public float TextTypingDuration(string _string) => (_string.Length * DefaultPerLetterDuration);
        public void TypeTextCancelFalse(string _string, params TextMeshProUGUI[] _text)
        {
            typeTextCancelFalseCoroutine = TypeTextCancelFalseCoroutine(_string, _text);
            StartCoroutine(typeTextCancelFalseCoroutine);
        }
        public void TypeTextCancelTrue(string _string, params TextMeshProUGUI[] _text)
        {
            if (typeTextCancelTrueCoroutine != null)
            {
                StopCoroutine(typeTextCancelTrueCoroutine);
            }

            typeTextCancelTrueCoroutine = TypeTextCancelTrueCoroutine(_string, _text);
            StartCoroutine(typeTextCancelTrueCoroutine);
        }
        private IEnumerator TypeTextCancelFalseCoroutine(string _string, params TextMeshProUGUI[] _text)
        {
            var letterWait = new WaitForSeconds(DefaultPerLetterDuration);
            var sb = new StringBuilder();

            foreach (var letter in _string)
            {
                sb.Append(letter);
                foreach (var text in _text)
                {
                    text.SetText(sb);
                }
                yield return letterWait;
            }
            yield return null;
        }
        private IEnumerator TypeTextCancelTrueCoroutine(string _string, params TextMeshProUGUI[] _text)
        {
            var letterWait = new WaitForSeconds(DefaultPerLetterDuration);
            var sb = new StringBuilder();

            foreach (var letter in _string)
            {
                sb.Append(letter);
                foreach (var text in _text)
                {
                    text.SetText(sb);
                }
                yield return letterWait;
            }
            yield return null;
        }
    }
}