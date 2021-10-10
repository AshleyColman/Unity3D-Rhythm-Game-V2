namespace AudioScripts
{
    using StaticDataScripts;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class AudioManager : MonoBehaviour
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        private IEnumerator loadAudioCoroutine;

        public void PlayScheduledAudio(double _playTime) => AudioSource.PlayScheduled(AudioSettings.dspTime + _playTime);
        public void SetAudioTime(float _time) => AudioSource.time = _time;
        public void LoadAudio(string _beatmapFolderPath, float _audioStartTime)
        {
            if (loadAudioCoroutine != null)
            {
                StopCoroutine(loadAudioCoroutine);
            }

            loadAudioCoroutine = LoadAudioCoroutine(_beatmapFolderPath, _audioStartTime);
            StartCoroutine(loadAudioCoroutine);
        }
        private IEnumerator LoadAudioCoroutine(string _path, float _playTime = 0f)
        {
            AudioSource.gameObject.SetActive(false);
            UnloadAudioClip();

            if (string.IsNullOrEmpty(_path) == false)
            {
                _path += FileTypes.Audio;
                using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(_path, AudioType.OGGVORBIS))
                {
                    ((DownloadHandlerAudioClip)uwr.downloadHandler).streamAudio = true;
                    yield return uwr.SendWebRequest();

                    if (uwr.result == UnityWebRequest.Result.ConnectionError ||
                      uwr.result == UnityWebRequest.Result.DataProcessingError ||
                      uwr.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log("Error loading audio");
                    }
                    else
                    {
                        AudioSource.clip = DownloadHandlerAudioClip.GetContent(uwr);
                    }
                }
            }
            AudioSource.gameObject.SetActive(false);
            yield return null;
        }
        private void UnloadAudioClip() 
        {
            if (AudioSource.clip != null)
            {
                AudioSource.clip.UnloadAudioData();
            }
        }
    }
}
