namespace ModeMenuScripts
{
    using StaticDataScripts;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class ModeMenuBackground : MonoBehaviour
    {
        public void TransitionIn()
        {
            LeanTween.cancel(this.gameObject);
            this.gameObject.transform.localScale = VectorValues.Vector0_75;
            this.gameObject.SetActive(true);
            LeanTween.scale(this.gameObject, Vector3.one, 0.25f).setEaseOutExpo();
        }
        public void TransitionOut()
        {
            this.gameObject.SetActive(false);
        }
    }
}