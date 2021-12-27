namespace UIScripts
{
    using UnityEngine;

    public sealed class MaterialLoader : MonoBehaviour
    {
        public Material DefaultMaterial { get; private set; }
        [SerializeField] private Shader shader = default;

        public Material GetNewMaterial() 
        {
            FindShaderIfNull();
            return new Material(shader);
        }
        private void FindShaderIfNull()
        {
            if (shader == null)
            {
                shader = Shader.Find("UI/Unlit/Transparent");
            }
        }
        private void SetDefaultMaterial()
        {
            FindShaderIfNull();
            DefaultMaterial = new Material(shader);
        }
    }
}