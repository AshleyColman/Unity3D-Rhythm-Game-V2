namespace AudioScripts
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class AudioManager : MonoBehaviour
    {
        public const byte Select1AudioClipIndex = 0;
        public const float AudioClipLoadDelayDuration = 0.1f;
        private double songAudioStartTime = 0;
        private float songAudioSourceVolume = 1f;
        private float userInterfaceAudioSourceVolume = 1f;
        private bool hasPaused = false;
        [SerializeField] private AudioSource songAudioSource = default;
        [SerializeField] private AudioSource userInterfaceAudioSource = default;
        [SerializeField] private AudioClip[] userInterfaceAudioClipArray = default;
        private IEnumerator loadSongAudioClipFromFileCoroutine;
        private IEnumerator checkToLoopAudioCoroutine;
        private IEnumerator playAudioAndTimeManagerFromStartTimeCoroutine;
        private TimeManager timeManager;

        public void PlayScheduledSongAudio(double _timeToPlay)
        {
            songAudioStartTime = AudioSettings.dspTime + _timeToPlay;
            songAudioSource.PlayScheduled(songAudioStartTime);
        }
        public void SetAudioStartTime(float _audioStartTime)
        {
            songAudioSource.time = _audioStartTime;
        }
        public void PlayOneShotUserInterfaceAudioSource(byte _clipIndex)
        {
            userInterfaceAudioSource.PlayOneShot(userInterfaceAudioClipArray[_clipIndex], userInterfaceAudioSourceVolume);
        }
        public void LoadSongAudioClipFromFile(string _beatmapFolderPath, float _audioStartTime, TimeManager timeManager,
            SongSlider _songSlider)
        {
            if (loadSongAudioClipFromFileCoroutine != null)
            {
                StopCoroutine(loadSongAudioClipFromFileCoroutine);
            }

            loadSongAudioClipFromFileCoroutine = LoadSongAudioClipFromFileCoroutine(_beatmapFolderPath, _audioStartTime,
                timeManager, _songSlider);
            StartCoroutine(loadSongAudioClipFromFileCoroutine);
        }
        private void Awake()
        {
            timeManager = FindObjectOfType<TimeManager>();
            notification = FindObjectOfType<Notification>();
            songAudioSource.volume = songAudioSourceVolume;
            userInterfaceAudioSource.volume = userInterfaceAudioSourceVolume;
        }
        private IEnumerator LoadSongAudioClipFromFileCoroutine(string _beatmapFolderPath, float _audioStartTime,
            TimeManager _timeManager, SongSlider _songSlider)
        {
            DeactivateSongAudioSource();
            UnloadSongAudioClip();
            _timeManager.StopTimer();

            if (string.IsNullOrEmpty(_beatmapFolderPath) == false)
            {
                string audioFilePath = string.Empty;
                bool hasLoadedAudioFile = false;

                for (byte i = 0; i < FileTypes.AudioFileTypesArray.Length; i++)
                {
                    audioFilePath = $"{_beatmapFolderPath}/{FileTypes.AudioFileTypesArray[i]}";

                    using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioFilePath, AudioType.OGGVORBIS))
                    {
                        ((DownloadHandlerAudioClip)www.downloadHandler).streamAudio = true;
                        yield return www.SendWebRequest();

                        if (www.isNetworkError || www.isHttpError)
                        {
                            hasLoadedAudioFile = false;
                        }
                        else
                        {
                            songAudioSource.clip = DownloadHandlerAudioClip.GetContent(www);

                            yield return new WaitForSeconds(AudioClipLoadDelayDuration);

                            PlayAudioAndTimeManagerFromStartTime(_audioStartTime, _timeManager);

                            yield return new WaitForSeconds(AudioClipLoadDelayDuration);

                            _songSlider.LerpSliderToValue(UtilityMethods.GetSliderValuePercentageFromTime(_audioStartTime,
                                songAudioSource.clip.length), AudioClipLoadDelayDuration);

                            yield return new WaitForSeconds(AudioClipLoadDelayDuration);

                            _songSlider.UpdateSongSliderProgress();

                            yield return new WaitForSeconds(AudioClipLoadDelayDuration);

                            hasLoadedAudioFile = true;
                        }
                    }

                    if (hasLoadedAudioFile == true)
                    {
                        break;
                    }
                    else
                    {
                        if (i == FileTypes.AudioFileTypesArray.Length)
                        {
                            DeactivateSongAudioSource();
                            DisplayErrorNotification(audioFilePath);
                        }
                        continue;
                    }
                }
            }
            else
            {
                DisplayErrorNotification("beatmap folder path null");
            }

            yield return null;
        }
        private void CheckToLoopAudio()
        {
            if (checkToLoopAudioCoroutine != null)
            {
                StopCoroutine(checkToLoopAudioCoroutine);
            }

            checkToLoopAudioCoroutine = CheckToLoopAudioCoroutine();
            StartCoroutine(checkToLoopAudioCoroutine);
        }
        private IEnumerator CheckToLoopAudioCoroutine()
        {
            while (songAudioSource.gameObject.activeSelf == true)
            {
                CheckIfAudioHasReachedEndOfClip();
                yield return null;
            }
            yield return null;
        }
        private void CheckIfAudioHasReachedEndOfClip()
        {
            if (songAudioSource.isPlaying == false)
            {
                if (hasPaused == false)
                {
                    PlayAudioAndTimeManagerFromStartTime(0f, timeManager);
                }
            }
        }
        private void PlayAudioAndTimeManagerFromStartTime(float _audioStartTime, TimeManager _timeManager)
        {
            if (playAudioAndTimeManagerFromStartTimeCoroutine != null)
            {
                StopCoroutine(playAudioAndTimeManagerFromStartTimeCoroutine);
            }

            playAudioAndTimeManagerFromStartTimeCoroutine = PlayAudioAndTimeManagerFromStartTimeCoroutine(_audioStartTime,
                _timeManager);

            StartCoroutine(playAudioAndTimeManagerFromStartTimeCoroutine);
        }
        private IEnumerator PlayAudioAndTimeManagerFromStartTimeCoroutine(float _audioStartTime, TimeManager _timeManager)
        {
            ActivateSongAudioSource();
            PlayScheduledSongAudio(AudioClipLoadDelayDuration);
            SetAudioStartTime(_audioStartTime);
            yield return new WaitForSeconds(AudioClipLoadDelayDuration);
            _timeManager.RecalculateAndPlayFromNewPosition();
            CheckToLoopAudio();
            yield return null;
        }
        private void DeactivateSongAudioSource()
        {
            songAudioSource.gameObject.SetActive(false);
        }
        private void ActivateSongAudioSource()
        {
            if (songAudioSource.gameObject.activeSelf == false)
            {
                songAudioSource.gameObject.SetActive(true);
            }
        }
        private void UnloadSongAudioClip()
        {
            if (songAudioSource.clip != null)
            {
                songAudioSource.clip.UnloadAudioData();
                //AudioClip.DestroyImmediate(songAudioSource.clip, true);
            }
        }
        private void DisplayErrorNotification(string _error)
        {
            notification.DisplayDescriptionNotification(ColorName.RED, "error loading beatmap audio", _error, 4f);
        }
    }
}
