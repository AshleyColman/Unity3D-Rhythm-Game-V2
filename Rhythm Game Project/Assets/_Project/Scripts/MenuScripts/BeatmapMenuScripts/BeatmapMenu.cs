namespace BeatmapMenuScripts 
{
    using System.Collections;
    using UnityEngine;

    public sealed class BeatmapMenu : Menu
    {
        [SerializeField] private Leaderboard leaderboard = default;
        [SerializeField] private FieldPanel fieldPanel = default;

        protected override IEnumerator TransitionInCoroutine()
        {
            leaderboard.RequestLeaderboard("beatmap");
            fieldPanel.SetPanel();
            yield return null;
        }
    }
}