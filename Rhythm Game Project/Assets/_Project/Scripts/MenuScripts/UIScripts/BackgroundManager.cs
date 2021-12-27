namespace UIScripts
{
    using StaticDataScripts;
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private Background[] backgroundArr = default;
        [SerializeField] private Image backgroundImage;
        [SerializeField] ImageLoader imageLoader = default;
        private byte backgroundIndex = 0;

        public Material BackgroundImageMaterial { get { return backgroundImage.material; } }

        public void LoadBackgroundImage(string _path) => imageLoader.LoadImage(ImageLoadType.File, false, _path, backgroundImage);
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
