namespace UIScripts
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private Background[] backgroundArr = default;
        //[SerializeField] ImageLoader imageLoader = default;
        private byte backgroundIndex = 0;

        public void TransitionNextImage()
        {
            backgroundArr[backgroundIndex].TransitionOut();
            CheckBackgroundIndex();
            backgroundArr[backgroundIndex].TransitionIn();
        }

        private void CheckBackgroundIndex()
        {
            if (backgroundIndex == backgroundArr.Length)
            {
                backgroundIndex = 0;
            }
            else
            {
                backgroundIndex++;
            }
        }
    }
}
