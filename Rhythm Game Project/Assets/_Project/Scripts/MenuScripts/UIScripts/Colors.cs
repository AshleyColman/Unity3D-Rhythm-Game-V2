namespace UIScripts
{
    using StaticDataScripts;
    using UnityEngine;

    public sealed class Colors : MonoBehaviour
    {
        [SerializeField] Color32 white = default;
        [SerializeField] Color32 grey = default;
        [SerializeField] Color32 black = default;
        [SerializeField] Color32 red = default;
        [SerializeField] Color32 lightGreen = default;
        [SerializeField] Color32 darkBlue = default;
        [SerializeField] Color32 lightBlue = default;
        [SerializeField] Color32 orange = default;
        [SerializeField] Color32 yellow = default;
        [SerializeField] Color32 pink = default;
        [SerializeField] Color32 purple = default;

        public Color32 GetColor(ColorType _color, byte _opacity = 100)
        {
            var result = _color switch
            {
                ColorType.white => white,
                ColorType.grey => grey,
                ColorType.red => red,
                ColorType.lightGreen => lightGreen,
                ColorType.darkBlue => darkBlue,
                ColorType.lightBlue => lightBlue,
                ColorType.orange => orange,
                ColorType.yellow => yellow,
                ColorType.pink => pink,
                ColorType.purple => purple,
                _ => black,
            };
            result = new Color32(result.r, result.g, result.b, _opacity);
            return result;
        }
    }
}
