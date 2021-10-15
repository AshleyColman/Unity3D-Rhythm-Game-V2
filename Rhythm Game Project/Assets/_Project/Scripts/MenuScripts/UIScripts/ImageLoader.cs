namespace UIScripts
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;

    public sealed class ImageLoader : MonoBehaviour
    {
        [SerializeField] private MaterialLoader materialLoader = default;

        public void LoadCompressedImage(string _url, Image _image)
        {
            StartCoroutine(LoadCompressedImageCoroutine(_url, _image));
        }
        private IEnumerator LoadCompressedImageCoroutine(string _url, Image _image)
        {
            if (string.IsNullOrEmpty(_url))
            {
                SetToDefaultMaterial(_image);
            }
            else
            {
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_url))
                {
                    yield return uwr.SendWebRequest();

                    if (uwr.result == UnityWebRequest.Result.ConnectionError ||
                        uwr.result == UnityWebRequest.Result.DataProcessingError ||
                        uwr.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log("Error loading image");
                    }
                    else
                    {
                        Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(uwr);
                        SetPerformance(downloadedTexture);
                        ApplyMaterial(_image, downloadedTexture);
                    }
                }
            }

            yield return null;
        }
        private void SetPerformance(Texture2D _texture)
        {
            _texture.filterMode = FilterMode.Trilinear;
            _texture.wrapMode = TextureWrapMode.Clamp;
            _texture.Compress(true);
            _texture.Apply(true);
        }
        private void ApplyMaterial(Image _image, Texture2D _texture)
        {
            _image.material = materialLoader.GetNewMaterial();
            _image.material.mainTexture = _texture;
        }
        private void SetToDefaultMaterial(Image _image) => _image.material = materialLoader.DefaultMaterial;
    }
}