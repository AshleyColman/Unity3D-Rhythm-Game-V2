namespace GameplayScripts 
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class ScoreEffect : MonoBehaviour
    {
        private const int LeftMaxTargetPositionX = -100;
        private const int RightMaxTargetPositionX = 100;
        [SerializeField] private TextMeshProUGUI[] leftTextArr = default;
        [SerializeField] private TextMeshProUGUI[] rightTextArr = default;
        private Transform[] leftTransformArr;
        private Transform[] rightTransformArr;
        private CanvasGroup[] leftCanvasGroupArr;
        private CanvasGroup[] rightCanvasGroupArr;
        private Vector3 leftStartingPosition = Vector3.zero;
        private Vector3 rightStartingPosition = Vector3.zero;
        private byte leftArrIndex = 0;
        private byte rightArrIndex = 0;
        private bool playLeft = false;

        public void PlayScoreEffect(int _score)
        {
            if (playLeft == true)
            {
                CheckLeftArrIndex();
                leftTextArr[leftArrIndex].SetText($"{_score}+");
                PlayLeftScoreEffectTween();
                leftArrIndex++;
                playLeft = false;
                return;
            }
            CheckRightArrIndex();
            rightTextArr[rightArrIndex].SetText($"+{_score}"); ;
            PlayRightScoreEffectTween();
            rightArrIndex++;
            playLeft = true;
        }
        private void Awake()
        {
            SetArrays();
            SetStartingPositions();
        }
        private void SetStartingPositions()
        {
            leftStartingPosition = leftTransformArr[0].localPosition;
            rightStartingPosition = rightTransformArr[0].localPosition;
        }
        private void CheckLeftArrIndex()
        {
            if (leftArrIndex >= leftTextArr.Length)
            {
                leftArrIndex = 0;
            }
        }
        private void CheckRightArrIndex()
        {
            if (rightArrIndex >= rightTextArr.Length)
            {
                rightArrIndex = 0;
            }
        }
        private void SetArrays()
        {
            rightTransformArr = new Transform[rightTextArr.Length];
            leftTransformArr = new Transform[leftTextArr.Length];

            rightCanvasGroupArr = new CanvasGroup[rightTextArr.Length];
            leftCanvasGroupArr = new CanvasGroup[leftTextArr.Length];

            for (byte i = 0; i < rightTextArr.Length; i++)
            {
                rightTransformArr[i] = rightTextArr[i].transform;
                rightCanvasGroupArr[i] = rightTextArr[i].GetComponent<CanvasGroup>();

                leftTransformArr[i] = leftTextArr[i].transform;
                leftCanvasGroupArr[i] = leftTextArr[i].GetComponent<CanvasGroup>();
            }
        }
        private void PlayLeftScoreEffectTween()
        {
            LeanTween.cancel(leftTextArr[leftArrIndex].gameObject);
            leftTransformArr[leftArrIndex].localPosition = leftStartingPosition;
            leftCanvasGroupArr[leftArrIndex].alpha = 0f;

            Vector3 endPosition = new Vector3((leftStartingPosition.x + LeftMaxTargetPositionX),
                leftStartingPosition.y, leftStartingPosition.z);

            LeanTween.moveLocal(leftTextArr[leftArrIndex].gameObject, endPosition, 1f).setEaseOutExpo();
            LeanTween.alphaCanvas(leftCanvasGroupArr[leftArrIndex], 1f,
                0.5f).setEaseOutExpo().setLoopPingPong(1);
        }
        private void PlayRightScoreEffectTween()
        {
            LeanTween.cancel(rightTextArr[rightArrIndex].gameObject);
            rightTransformArr[rightArrIndex].localPosition = rightStartingPosition;
            rightCanvasGroupArr[rightArrIndex].alpha = 0f;

            Vector3 endPosition = new Vector3((rightStartingPosition.x + RightMaxTargetPositionX),
                rightStartingPosition.y, rightStartingPosition.z);

            LeanTween.moveLocal(rightTextArr[rightArrIndex].gameObject, endPosition, 1f).setEaseOutExpo();
            LeanTween.alphaCanvas(rightCanvasGroupArr[rightArrIndex], 1f,
                0.5f).setEaseOutExpo().setLoopPingPong(1);
        }
    }
}