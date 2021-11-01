namespace UIScripts
{
    using System.Collections;
    using TMPro;
    using UnityEngine;

    public sealed class LoadingIcon : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text1 = default;
        [SerializeField] private TextMeshProUGUI text2 = default;
        [SerializeField] private TextMeshProUGUI text3 = default;
        [SerializeField] private TextMeshProUGUI text4 = default;
        [SerializeField] private GameObject textContainer = default;
        private IEnumerator showCoroutine;

        private void Start()
        {
            Activate();
        }
        public void Activate()
        {
            if (showCoroutine != null)
            {
                StopCoroutine(showCoroutine);
            }
            showCoroutine = ShowCoroutine();
            StartCoroutine(showCoroutine);
        }
        public void Deactivate() => textContainer.SetActive(false);
        private IEnumerator ShowCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.25f);
            text1.gameObject.SetActive(true);
            text2.gameObject.SetActive(false);
            text3.gameObject.SetActive(false);
            text4.gameObject.SetActive(false);
            textContainer.SetActive(true);
            yield return wait;

            while (this.gameObject.activeSelf == true)
            {
                text1.gameObject.SetActive(false);
                text2.gameObject.SetActive(true);
                yield return wait;
                text2.gameObject.SetActive(false);
                text3.gameObject.SetActive(true);
                yield return wait;
                text3.gameObject.SetActive(false);
                text4.gameObject.SetActive(true);
                yield return wait;
                text4.gameObject.SetActive(false);
                text1.gameObject.SetActive(true);
                yield return wait;
            }
            yield return null;
        }
    }
}