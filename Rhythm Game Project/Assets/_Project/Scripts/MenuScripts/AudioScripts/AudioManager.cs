namespace AudioScripts
{
    using AllMenuScripts;
    using StaticDataScripts;
    using System;
    using System.Collections;
    using System.IO;
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
        public void LoadAudio(string _directory, float _playTime)
        {
            if (loadAudioCoroutine != null)
            {
                StopCoroutine(loadAudioCoroutine);
            }
            loadAudioCoroutine = LoadAudioCoroutine(_directory, _playTime);
            StartCoroutine(loadAudioCoroutine);
        }
        private IEnumerator LoadAudioCoroutine(string _path, float _playTime = 0f)
        {
            SongAudioSource.gameObject.SetActive(false);
            UnloadAudioClip();
            if (string.IsNullOrEmpty(_path) == false)
            {
                using UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(_path, GetAudioTypeFromPath(_path));
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
            SongAudioSource.gameObject.SetActive(true);
            yield return null;
        }
        private AudioType GetAudioTypeFromPath(string _path)
        {
            if (Path.GetExtension(_path) == FileTypes.Audio[0])
            {
                return AudioType.MPEG;
            }
            return AudioType.OGGVORBIS;
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
