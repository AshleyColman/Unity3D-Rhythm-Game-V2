

using StaticDataScripts;
using UIScripts;
using UnityEngine;

public static class JudgementData
{
    public static readonly int OkayScore = 50;
    public static readonly int GreatScore = 100;
    public static readonly int PerfectScore = 250;
    public static readonly int MissScore = 0;
    public static int GetJudgementScore(Judgement _judgement)
    {
        int score = _judgement switch
        {
            Judgement.Okay => OkayScore,
            Judgement.Great => GreatScore,
            Judgement.Perfect => PerfectScore,
            _ => MissScore
        };
        return score;
    }
    public static Color32 GetJudgementColor(Judgement _judgement)
    {
        Color32 color = _judgement switch
        {
            Judgement.Okay => Colors.Pink,
            Judgement.Great => Colors.DarkBlue,
            Judgement.Perfect => Colors.Yellow,
            _ => Colors.Red
        };
        return color;
    }
}
