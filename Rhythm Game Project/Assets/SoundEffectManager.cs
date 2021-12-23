namespace GameplayScripts 
{
    using System;
    using UnityEngine;

    public sealed class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audioSourceArr = default;
        [SerializeField] private AudioClip hitClip = default;
        [SerializeField] private AudioClip missClip = default;
        private int sourceArrIndex = 0;
        public void PlayHitEffect() => PlayEffect(() => audioSourceArr[sourceArrIndex].PlayOneShot(hitClip));
        public void PlayMissEffect() => PlayEffect(() => audioSourceArr[sourceArrIndex].PlayOneShot(missClip));
        private void PlayEffect(Action _action)
        {
            CheckSourceArrIndex();
            _action();
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
