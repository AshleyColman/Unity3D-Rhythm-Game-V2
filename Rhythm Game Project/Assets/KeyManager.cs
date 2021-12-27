namespace GameplayScripts 
{
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class KeyManager : MonoBehaviour
    {
        [SerializeField] private Key keyS = default;
        [SerializeField] private Key keyD = default;
        [SerializeField] private Key keyF = default;
        [SerializeField] private Key keyJ = default;
        [SerializeField] private Key keyK = default;
        [SerializeField] private Key keyL = default;
        [SerializeField] private Key keySpace = default;

        [field: SerializeField] public Key[] KeyArr { get; private set; } = default;

        public void DisableKeys(Mode _mode)
        {
            switch (_mode)
            {
                case Mode.One:
                    DisableKeys(keyS, keyD, keyJ, keyK, keyL);
                    break;
                case Mode.Two:
                    DisableKeys(keyS, keyD, keyK, keyL);
                    break;
                case Mode.Three:
                    DisableKeys(keyS, keyK, keyL);
                    break;
                case Mode.Four:
                    DisableKeys(keyS, keyL);
                    break;
                case Mode.Five:
                    DisableKeys(keyL);
                    break;
                case Mode.Six:
                    break;
            }
        }
        private void DisableKeys(params Key[] _keys)
        {
            foreach (var key in _keys)
            {
                key.DisableKey();
            }
        }
    }
}