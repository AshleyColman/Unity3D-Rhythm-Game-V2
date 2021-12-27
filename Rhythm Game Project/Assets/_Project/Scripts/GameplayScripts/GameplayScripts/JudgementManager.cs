namespace GameplayScripts
{
    using StaticDataScripts;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public sealed class JudgementManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI perfectText = default;
        [SerializeField] private TextMeshProUGUI greatText = default;
        [SerializeField] private TextMeshProUGUI okayText = default;
        [SerializeField] private TextMeshProUGUI missText = default;
        private int perfect = 0;
        private int great = 0;
        private int okay = 0;
        private int miss = 0;

        public void IncrementJudgement(Judgement _judgement)
        {
            switch (_judgement)
            {
                case Judgement.Okay:
                    Increment(ref okay, okayText);
                    break;
                case Judgement.Great:
                    Increment(ref great, greatText);
                    break;
                case Judgement.Perfect:
                    Increment(ref perfect, perfectText);
                    break;
                case Judgement.Miss:
                    Increment(ref miss, missText);
                    break;
                default:
                    Increment(ref miss, missText);
                    break;
            }
        }
        private void Increment(ref int _judgementCount, TextMeshProUGUI _text)
        {
            _judgementCount++;
            _text.SetText(_judgementCount.ToString());
        }
    }
}