namespace ListMenuScripts
{
    using FileScripts;
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.UI.Extensions;

    public sealed class ButtonList : MonoBehaviour
    {
        [SerializeField] private FileManager fileManager = default;
        [SerializeField] private BeatmapButton buttonPrefab = default;
        [SerializeField] private ScrollRect scrollRect = default;
        [SerializeField] private UI_ScrollRectOcclusion scrollRectOcclusion = default;
        [SerializeField] private ImageLoader imageLoader = default;
        [SerializeField] private Transform disabledContent = default;
        [SerializeField] private ContentSizeFitter contentSizeFitter = default;
        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup = default;
        [SerializeField] private LoadingIcon loadingIcon = default;
        private IEnumerator instantiateNewListCoroutine;
        private IEnumerator activateListCoroutine;

        public List<BeatmapButton> BeatmapButtonList { get; private set; } = new List<BeatmapButton>();

        public bool HasInstantiatedButtonList => BeatmapButtonList.Count > 0 ? true : false;
        public void InstantiateNewList()
        {
            if (instantiateNewListCoroutine != null)
            {
                StopCoroutine(instantiateNewListCoroutine);
            }
            instantiateNewListCoroutine = InstantiateNewListCoroutine();
            StartCoroutine(instantiateNewListCoroutine);
        }
        public void ActivateList()
        {
            if (activateListCoroutine != null)
            {
                StopCoroutine(activateListCoroutine);
            }
            activateListCoroutine = ActivateListCoroutine();
            StartCoroutine(activateListCoroutine);
        }
        public void DeactivateContent()
        {
            scrollRect.content.gameObject.SetActive(false);
            loadingIcon.Activate();
        }
        public void ActivateContent()
        {
            scrollRect.content.gameObject.SetActive(true);
            loadingIcon.Deactivate();
        }
        public void SetButtonTransformToDisabled(BeatmapButton _button)
        {
            if (_button.transform.parent != disabledContent)
            {
                _button.transform.parent = disabledContent;
            }
        }
        public void SetButtonTransformToActive(BeatmapButton _button)
        {
            if (_button.transform.parent != scrollRect.content)
            {
                _button.transform.parent = scrollRect.content;
            }
        }
        private IEnumerator InstantiateNewListCoroutine()
        {
            WaitForSeconds instantiateWait = new WaitForSeconds(0.1f);
            WaitForSeconds activateWait = new WaitForSeconds(2f);
            DeactivateContent();
            int index = 0;
            foreach (var directory in fileManager.BeatmapDirectoryArr)
            {
                Beatmap beatmap = fileManager.Load($"{directory}/{FileNames.Beatmap}{FileTypes.BeatmapFileType}");
                if (beatmap != null)
                {
                    BeatmapButton button = Instantiate(buttonPrefab, scrollRect.content);
                    BeatmapButtonList.Add(button);
                    imageLoader.LoadImage(ImageLoadType.File, true, $"{directory}/{FileNames.Image}{FileTypes.Image}", button.BeatmapImage);
                    button.SetButton((index+1).ToString(), beatmap.Title, beatmap.Artist, beatmap.Creator, beatmap.CreatedDate.ToString(), beatmap.Mode, Colors.GetModeColor(beatmap.Mode));
                    yield return instantiateWait;
                }
                index++;
            }
            yield return activateWait;
            ActivateList();
            SelectFirstButton();
        }
        private void SelectFirstButton()
        {
            if (BeatmapButtonList.Count != 0)
            {
                BeatmapButtonList[0].Button_OnSelect();
            }
        }
        private IEnumerator ActivateListCoroutine()
        {
            ActivateContent();
            yield return new WaitForEndOfFrame();
            contentSizeFitter.enabled = true;
            verticalLayoutGroup.enabled = true;
            yield return new WaitForEndOfFrame();
            contentSizeFitter.enabled = false;
            verticalLayoutGroup.enabled = false;
            yield return new WaitForEndOfFrame();
            scrollRect.ScrollToTop();
            yield return new WaitForEndOfFrame();
            scrollRectOcclusion.Init();
            yield return null;
        }
    }
}