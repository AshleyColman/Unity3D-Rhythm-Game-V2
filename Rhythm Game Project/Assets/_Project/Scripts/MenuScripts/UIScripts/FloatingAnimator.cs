namespace UIScripts
{
    using UnityEngine;

    public sealed class FloatingAnimator : MonoBehaviour
    {
        [SerializeField] private float toPositionY = default;
        [SerializeField] private float time = default;

        private void OnEnable()
        {
            PlayFloatTweenLoop();
        }

        public void PlayFloatTweenLoop()
        {
            LeanTween.cancel(this.gameObject);
            LeanTween.moveLocalY(this.gameObject, toPositionY, time).setLoopPingPong(-1).setEaseInOutQuad();
        }
    }
}