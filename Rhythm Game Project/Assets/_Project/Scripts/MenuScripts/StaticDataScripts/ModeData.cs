namespace StaticDataScripts
{
    public static class ModeData
    {
        public static string GetModeString(Mode _mode)
        {
            var modeString = _mode switch
            {
                Mode.One => "1C",
                Mode.Two => "2C",
                Mode.Three => "3C",
                Mode.Four => "4C",
                Mode.Five => "5C",
                Mode.Six => "6C",
                _ => "0C"
            };
            return modeString;
        }
    }
}