namespace GameplayScripts
{
    using AllMenuScripts;
    using UnityEngine;

    public sealed class GameplayRhythmEffects : RhythmEffects
    {
        [SerializeField] private HitObjectFollower hitObjectFollower = default;

        public override void OnMeasure()
        {
        }
        public override void OnTick()
        {
            hitObjectFollower.PlayRhythmTween();
        }
    }
}