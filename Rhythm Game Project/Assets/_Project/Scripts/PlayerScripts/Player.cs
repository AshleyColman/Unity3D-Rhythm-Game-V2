namespace PlayerScripts
{
    using UIScripts;
    using UnityEngine;

    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private MaterialLoader materialLoader = default;
        public Material ProfileImageMaterial { get; private set; }

        public string Username { get; set; } = "Ashley";
        public byte Level { get; }
        public bool LoggedIn { get; set; }

        private void Awake() => ProfileImageMaterial = materialLoader.GetNewMaterial();
    }
}