namespace UIScripts
{
    using TMPro;
    using UnityEngine;

    public sealed class RankData : MonoBehaviour
    {
        [field: SerializeField] public Rank rankS { get; private set; }
        [field: SerializeField] public Rank rankA { get; private set; }
        [field: SerializeField] public Rank rankB { get; private set; }
        [field: SerializeField] public Rank rankC { get; private set; }
        [field: SerializeField] public Rank rankD { get; private set; }
        [field: SerializeField] public Rank rankE { get; private set; }
        [field: SerializeField] public Rank rankF { get; private set; }
        [field: SerializeField] public Rank rankX { get; private set; }

        public Rank GetRank(string _rank = "X")
        {
            var rank = _rank switch
            {
                "S" => rankS,
                "A" => rankA,
                "B" => rankB,
                "C" => rankC,
                "D" => rankD,
                "E" => rankE,
                "F" => rankF,
                "X" => rankX,
                _ => rankX
            };
            return rank;
        }
        public Rank GetRank(float _accuracy)
        {
            if (_accuracy == rankS.ValueToAchieve)
            {
                return rankS;
            }
            else if (_accuracy < rankS.ValueToAchieve && _accuracy >= rankA.ValueToAchieve)
            {
                return rankA;
            }
            else if (_accuracy < rankA.ValueToAchieve && _accuracy >= rankB.ValueToAchieve)
            {
                return rankB;
            }
            else if (_accuracy < rankB.ValueToAchieve && _accuracy >= rankC.ValueToAchieve)
            {
                return rankC;
            }
            else if (_accuracy < rankC.ValueToAchieve && _accuracy >= rankD.ValueToAchieve)
            {
                return rankD;
            }
            else if (_accuracy < rankD.ValueToAchieve && _accuracy >= rankE.ValueToAchieve)
            {
                return rankE;
            }
            else
            {
                return rankF;
            }
        }
    }
}