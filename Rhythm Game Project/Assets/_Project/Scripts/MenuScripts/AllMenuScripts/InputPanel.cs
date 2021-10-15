namespace AllMenuScripts
{
    using TMPro;
    using UnityEngine;

    public sealed class InputPanel : MonoBehaviour
    {
        private const byte TextSizeMultiplier = 16;
        [SerializeField] private GameObject inputPanel = default;
        [SerializeField] private Transform textPanelTransform = default;
        [SerializeField] private GameObject[] spacingArr = default;
        [SerializeField] private TextMeshProUGUI[] inputTextArr = default;
        [SerializeField] private TextMeshProUGUI[] keyTextArr = default;
        [SerializeField] private CanvasGroup canvasGroup = default;
        private RectTransform[] inputTextRectTransformArr;
        private RectTransform[] keyTextRectTransformArr;
        private Vector3 endPosition;
        private Vector3 startPosition;

        public void TransitionIn() => inputPanel.gameObject.SetActive(true);
        public void SetInputText (string[] _controlTextArr, string[] _keyTextArr)
        {
            inputPanel.gameObject.SetActive(false);

            for (byte i = 0; i < inputTextArr.Length; i++)
            {
                if (i >= _controlTextArr.Length)
                {
                    DeactivateObjectIfActive(inputTextArr[i].gameObject);

                    if (i < spacingArr.Length)
                    {
                        DeactivateObjectIfActive(spacingArr[i]);
                    }
                }
                else
                {
                    inputTextArr[i].SetText(_controlTextArr[i]);
                    ActivateObjectIfActive(inputTextArr[i].gameObject);

                    if (i < spacingArr.Length)
                    {
                        ActivateObjectIfActive(spacingArr[i]);
                    }
                }
            }

            for (byte i = 0; i < keyTextArr.Length; i++)
            {
                if (i >= _keyTextArr.Length)
                {
                    DeactivateObjectIfActive(keyTextArr[i].gameObject);
                }
                else
                {
                    keyTextArr[i].SetText(_keyTextArr[i]);
                    ActivateObjectIfActive(keyTextArr[i].gameObject);
                }
            }

            UpdateTextSizes();
            inputPanel.gameObject.SetActive(true);
            PlayDisplayTween();
        }
        private void Awake()
        {
            SetRectTransforms();
            endPosition = textPanelTransform.localPosition;
            startPosition = new Vector3(endPosition.x, endPosition.y - 50f, endPosition.z);
        }
        private void SetRectTransforms()
        {
            inputTextRectTransformArr = new RectTransform[inputTextArr.Length];
            keyTextRectTransformArr = new RectTransform[keyTextArr.Length];
            for (byte i = 0; i < inputTextArr.Length; i++)
            {
                inputTextRectTransformArr[i] = inputTextArr[i].rectTransform;
                keyTextRectTransformArr[i] = keyTextArr[i].rectTransform;
            }
        }
        private void UpdateTextSizes()
        {
            for (byte i = 0; i < inputTextArr.Length; i++)
            {
                inputTextRectTransformArr[i].sizeDelta = new Vector2(inputTextArr[i].text.Length * TextSizeMultiplier,
                    inputTextRectTransformArr[i].sizeDelta.y);

                keyTextRectTransformArr[i].sizeDelta = new Vector2(keyTextArr[i].text.Length * TextSizeMultiplier,
                    keyTextRectTransformArr[i].sizeDelta.y);
            }
        }
        private void DeactivateObjectIfActive(GameObject _object)
        {
            if (_object.gameObject.activeSelf == true)
            {
                _object.gameObject.SetActive(false);
            }
        }
        private void ActivateObjectIfActive(GameObject _object)
        {
            if (_object.gameObject.activeSelf == false)
            {
                _object.gameObject.SetActive(true);
            }
        }
        private void PlayDisplayTween()
        {
            LeanTween.cancel(textPanelTransform.gameObject);
            textPanelTransform.localPosition = startPosition;
            LeanTween.cancel(canvasGroup.gameObject);
            canvasGroup.alpha = 0f;
            LeanTween.alphaCanvas(canvasGroup, 1f, 2f).setEaseOutExpo();
            LeanTween.moveLocalY(textPanelTransform.gameObject, endPosition.y, 1f).setEaseOutExpo();
        }
    }
}