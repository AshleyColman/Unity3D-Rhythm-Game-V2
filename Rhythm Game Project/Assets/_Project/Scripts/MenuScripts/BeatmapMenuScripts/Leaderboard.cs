namespace BeatmapMenuScripts
{
    using DatabaseScripts;
    using StaticDataScripts;
    using System;
    using System.Collections;
    using System.Threading;
    using System.Threading.Tasks;
    using TMPro;
    using UIScripts;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class Leaderboard : MonoBehaviour
    {
        [SerializeField] private LeaderboardButton[] buttonArr = default;
        [SerializeField] private Database database = default;
        [SerializeField] private RankData rankData = default;
        [SerializeField] private ScrollRect scrollRect = default;
        [SerializeField] private LoadingIcon loadingIcon = default;
        [SerializeField] private ImageLoader imageLoader = default;
        private IEnumerator requestLeaderboardCoroutine;
        private IEnumerator waitToActivateCoroutine;
        private IEnumerator playFlashLoopCoroutine;
        private int imageCount = 0;

        private void Awake() => SetButtonPositions();
        public void RequestLeaderboard(string _databaseTable)
        {
            StopAllLeaderboardCoroutines();
            Deactivate();
            for (int buttonIndex = 0; buttonIndex < buttonArr.Length; buttonIndex++)
            {
                string[] data = database.GetLeaderboardData(_databaseTable, buttonIndex);
                SetButton(data, buttonIndex);
            }
        }
        private void SetButtonPositions()
        {
            int position = 1;
            foreach (var button in buttonArr)
            {
                button.SetButtonPosition($"{position}#");
                position++;
            }
        }
        private void SetButton(string[] _data, int _buttonIndex)
        {
            if (_data.Length == 0 || _data == null) 
            {
                buttonArr[_buttonIndex].SetButtonNoData();
                imageLoader.LoadImage(ImageLoadType.Url, true, string.Empty, buttonArr[_buttonIndex].playerImage,
                   IncrementImageCounter);
            }
            else 
            {
                if (string.IsNullOrEmpty(_data[5]) == false)
                {
                    imageLoader.LoadImage(ImageLoadType.Url, true, _data[5], buttonArr[_buttonIndex].playerImage,
                        IncrementImageCounter);
                }
                buttonArr[_buttonIndex].SetButton(_data[0], _data[1], _data[2], _data[3], rankData.GetRank(_data[4]));
            }
        }
        private void Deactivate()
        {
            scrollRect.content.gameObject.SetActive(false);
            loadingIcon.Activate();
        }
        private void Activate()
        {
            scrollRect.content.gameObject.SetActive(true);
            loadingIcon.Deactivate();
            PlayFlashLoopTween();
        }
        private void IncrementImageCounter()
        {
            imageCount++;
            if (imageCount >= buttonArr.Length)
            {
                Activate();
            }
        }
        private void PlayFlashLoopTween()
        {
            if (playFlashLoopCoroutine != null)
            {
                StopCoroutine(playFlashLoopCoroutine);
            }
            playFlashLoopCoroutine = PlayFlashLoopTweenCoroutine();
            StartCoroutine(playFlashLoopCoroutine);
        }
        private IEnumerator PlayFlashLoopTweenCoroutine()
        {
            WaitForSeconds buttonFlashWait = new WaitForSeconds(0.1f);
            WaitForSeconds loopWait = new WaitForSeconds(2f);
            yield return loopWait;
            foreach (var button in buttonArr)
            {
                button.PlayFlashTween();
                yield return buttonFlashWait;
            }
            PlayFlashLoopTween();
            yield return null;
        }
        private void StopAllLeaderboardCoroutines()
        {
            StopAllCoroutines();
            imageLoader.StopAllCoroutines();
        }
    }
}