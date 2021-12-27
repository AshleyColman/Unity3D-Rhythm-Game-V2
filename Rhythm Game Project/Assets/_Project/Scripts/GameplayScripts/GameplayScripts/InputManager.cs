namespace GameplayScripts
{
    using System.Collections;
    using UnityEngine;

    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private BeatmapController beatmapController = default;
        [SerializeField] private FeverManager feverManager = default;
        [SerializeField] private KeyManager keyManager = default;
        private IEnumerator checkToRunGameplayCoroutine;

        private void Update()
        {
            CheckKeyUI();
            CheckFeverKey();
        }
        public void CheckToRunGameplay()
        {
            if (checkToRunGameplayCoroutine != null)
            {
                StopCoroutine(checkToRunGameplayCoroutine);
            }
            checkToRunGameplayCoroutine = CheckToRunGameplayCoroutine();
            StartCoroutine(checkToRunGameplayCoroutine);
        }
        private IEnumerator CheckToRunGameplayCoroutine()
        {
            while (beatmapController.IsInitialized == false)
            {
                if (Input.anyKeyDown)
                {
                    beatmapController.InitializeStart();
                }
                yield return null;
            }
            yield return null;
        }
        private void CheckKeyUI()
        {
            CheckKeyDown();
            CheckKeyUp();
        }
        private void CheckKeyDown()
        {
            if (Input.anyKey)
            {
                foreach (var key in keyManager.KeyArr)
                {
                    if (Input.GetKey(key.KeyCode))
                    {
                        key.PlayOnKeyAnimation();
                    }
                    if (Input.GetKeyDown(key.KeyCode))
                    {
                        key.PlayOnKeyDownAnimation();
                    }
                }
            }
        }
        private void CheckKeyUp()
        {
            foreach (var key in keyManager.KeyArr)
            {
                if (Input.GetKeyUp(key.KeyCode))
                {
                    key.PlayOnKeyReleaseAnimation();
                }
            }
        }
        private void CheckFeverKey()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (feverManager.CanActivate == true)
                {
                    feverManager.Activate();
                }
            }
        }
    }
}