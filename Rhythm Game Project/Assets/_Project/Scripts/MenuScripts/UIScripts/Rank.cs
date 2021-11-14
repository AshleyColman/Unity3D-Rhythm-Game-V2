namespace UIScripts
{
    using TMPro;
    using UnityEngine;

    public sealed class Rank : MonoBehaviour
    {
        [field: SerializeField] public TMP_ColorGradient ColorGradient { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public StaticDataScripts.Rank RankEnum { get; private set; }
    }
}