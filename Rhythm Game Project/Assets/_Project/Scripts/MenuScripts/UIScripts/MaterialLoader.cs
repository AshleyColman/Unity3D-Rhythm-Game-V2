namespace UIScripts
{
    using UnityEngine;

    public sealed class MaterialLoader : MonoBehaviour
    {
        public Material DefaultMaterial { get; private set; }
        private Shader imageShader;

        public Material GetNewMaterial() => new Material(imageShader);
        private void Awake()
        {
            SetImageShader();
        }
        private void SetImageShader() => imageShader = Shader.Find("UI/Unlit/Transparent");
        private void SetDefaultMaterial() => DefaultMaterial = new Material(imageShader);
    }
}