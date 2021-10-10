namespace PlayerScripts
{
    using UnityEngine;

    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private Material profileImageMaterial = default;

        public string Username { get; } = "Ashley";
        public byte Level { get; }
        public bool LoggedIn { get; }
    }
}