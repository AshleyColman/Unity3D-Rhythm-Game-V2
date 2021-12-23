namespace AudioScripts
{
    using StaticDataScripts;
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioReverbFilter filter = default;
        [field: SerializeField] public AudioSource SongAudioSource { get; private set; }

        private IEnumerator loadAudioCoroutine;

        public double SongAudioSourceTime { get { return SongAudioSource.time; } }
        public double SongAudioClipLength { get { return SongAudioSource.clip.length; } }

        public void EnableReverbFilter() => filter.enabled = true;
        public void DisableReverbFilter() => filter.enabled = false;
        public void PlayAudio(double _time) => SongAudioSource.PlayScheduled(AudioSettings.dspTime + _time);
        public void SetAudioTime(float _time) => SongAudioSource.time = _time;
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
            SongAudioSource.gameObject.SetActive(false);
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
                        SongAudioSource.clip = DownloadHandlerAudioClip.GetContent(uwr);
                    }
                }
            }
            SongAudioSource.gameObject.SetActive(false);
            yield return null;
        }
        private void UnloadAudioClip() 
        {
            if (SongAudioSource.clip != null)
            {
                SongAudioSource.clip.UnloadAudioData();
            }
        }

    }
}
