namespace GameplayScripts
{
    using System.Collections;
    using UnityEngine;

    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private BeatmapController beatmapController = default;
        [SerializeField] private Key[] keyArr = default;
        private IEnumerator checkToRunGameplayCoroutine;

        private void Update()
        {
            CheckKeyUI();
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
                foreach (var key in keyArr)
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
            foreach (var key in keyArr)
            {
                if (Input.GetKeyUp(key.KeyCode))
                {
                    key.PlayOnKeyReleaseAnimation();
                }
            }
        }
    }
}