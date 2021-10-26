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
        //[SerializeField] private BeatmapButton button2c = default;
        //[SerializeField] private BeatmapButton button3c = default;
        //[SerializeField] private BeatmapButton button4c = default;
        //[SerializeField] private BeatmapButton button5c = default;
        //[SerializeField] private BeatmapButton button6c = default;
        [SerializeField] private ScrollRect scrollRect = default;
        [SerializeField] private UI_ScrollRectOcclusion scrollRectOcclusion = default;
        [SerializeField] private ImageLoader imageLoader = default;
        private List<BeatmapButton> buttonList = new List<BeatmapButton>();
        private IEnumerator instantiateNewListCoroutine;
        private IEnumerator activateListCoroutine;

        public bool HasInstantiatedButtonList => buttonList.Count > 0 ? true : false;
        public void InstantiateNewList()
        {
            if (instantiateNewListCoroutine != null)
            {
                StopCoroutine(instantiateNewListCoroutine);
            }
            instantiateNewListCoroutine = InstantiateNewListCoroutine();
            StartCoroutine(instantiateNewListCoroutine);
        }
        private IEnumerator InstantiateNewListCoroutine()
        {
            WaitForSeconds instantiateWait = new WaitForSeconds(0.1f);
            WaitForSeconds activateWait = new WaitForSeconds(2f);
            DeactivateList();
            int index = 0;
            foreach (var directory in fileManager.BeatmapDirectoryArr)
            {
                Beatmap beatmap = fileManager.Load($"{directory}/{FileNames.Beatmap}{FileTypes.BeatmapFileType}");
                if (beatmap != null)
                {
                    BeatmapButton button = Instantiate(buttonPrefab, scrollRect.content);
                    buttonList.Add(button);
                    imageLoader.LoadCompressedImage(ImageLoadType.File, $"{directory}/{FileNames.Image}{FileTypes.Image}", button.BeatmapImage);
                    button.SetButton(index.ToString(), beatmap.Title, beatmap.Artist, beatmap.Creator, beatmap.CreatedDate.ToString(), beatmap.ModeName, Colors.GetModeColor(beatmap.Mode));
                    yield return instantiateWait;
                }
                index++;
            }
            yield return activateWait;
            ActivateList();
        }
        private void ActivateList()
        {
            if (activateListCoroutine != null)
            {
                StopCoroutine(activateListCoroutine);
            }
            activateListCoroutine = ActivateListCoroutine();
            StartCoroutine(activateListCoroutine);
        }
        private IEnumerator ActivateListCoroutine()
        {
            scrollRect.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            scrollRectOcclusion.Init();
            yield return new WaitForEndOfFrame();
            scrollRect.ScrollToTop();
            yield return null;
        }
        private void DeactivateList() => scrollRect.gameObject.SetActive(false);
    }
}