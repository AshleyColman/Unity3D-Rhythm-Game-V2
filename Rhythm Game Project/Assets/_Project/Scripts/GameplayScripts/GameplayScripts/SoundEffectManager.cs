namespace GameplayScripts 
{
    using System;
    using UnityEngine;

    public sealed class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audioSourceArr = default;

        [field: SerializeField] public AudioClip hitClip { get; private set; }
        [field: SerializeField] public AudioClip missClip { get; private set; }
        [field: SerializeField] public AudioClip bassClip { get; private set; }
        [field: SerializeField] public AudioClip select1Clip { get; private set; }
        [field: SerializeField] public AudioClip select2Clip { get; private set; }


        private int sourceArrIndex = 0;
        public void PlayEffect(AudioClip _clip)
        {
            CheckSourceArrIndex();
            audioSourceArr[sourceArrIndex].PlayOneShot(_clip);
            sourceArrIndex++;
        }
        private void CheckSourceArrIndex()
        {
            if (sourceArrIndex >= audioSourceArr.Length)
            {
                sourceArrIndex = 0;
            }
        }
    }
}
