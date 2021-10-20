namespace ModeMenuScripts
{
    using UnityEngine;

    public class ModeMenuBackgroundManager : MonoBehaviour
    {
        [SerializeField] private ModeMenuBackground[] backgroundArr = default;
        private int backgroundIndex = 0;

        public void TransitionToIndex(int _index)
        {
            backgroundArr[backgroundIndex].TransitionOut();
            backgroundArr[_index].TransitionIn();
            backgroundIndex = _index;
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