namespace StaticDataScripts
{ 
    public static class ApproachRateData
    {
        public static double GetApproachRate(ApproachRate _approachRate)
        {
            return _approachRate switch
            {
                ApproachRate.Fast => 0.5,
                ApproachRate.Normal => 1,
                ApproachRate.Slow => 1.5,
                _ => 100,
            };
        }
    }
}