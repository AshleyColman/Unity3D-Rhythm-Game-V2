namespace UIScripts
{
    using StaticDataScripts;
    using UnityEngine;

    public static class Colors
    {
        public static Color32 White = new Color32(255, 255, 255, 255);
        public static Color32 Grey = new Color32(200, 200, 200, 255);
        public static Color32 DarkGrey05 = new Color32(76, 76, 76, 128);
        public static Color32 Black = new Color32(0, 0, 0, 255);
        public static Color32 Red = new Color32(242, 16, 28, 255);
        public static Color32 Red_80 = new Color32(242, 16, 28, 204);
        public static Color32 Green = new Color32(110, 242, 16, 255);
        public static Color32 Green_80 = new Color32(110, 242, 16, 204);
        public static Color32 DarkBlue = new Color32(16, 122, 242, 255);
        public static Color32 LightBlue = new Color32(16, 240, 242, 255);
        public static Color32 Orange = new Color32(242, 169, 16, 255);
        public static Color32 Yellow = new Color32(242, 204, 16, 255);
        public static Color32 Pink = new Color32(189, 16, 242, 255);
        public static Color32 Purple = new Color32(125, 16, 242, 255);
        public static Color32[] ColorArr = new Color32[]
        {
            Red,
            Green,
            LightBlue,
            Orange,
            Yellow,
            Pink,
            Purple
        };
        public static Color32 GetModeColor(Mode _mode)
        {
            var color = _mode switch
            {
                Mode.One => Green,
                Mode.Two => LightBlue,
                Mode.Three => DarkBlue,
                Mode.Four => Purple,
                Mode.Five => Pink,
                Mode.Six => Orange,
                _ => Black
            };
            return color;
        }
    }
}
