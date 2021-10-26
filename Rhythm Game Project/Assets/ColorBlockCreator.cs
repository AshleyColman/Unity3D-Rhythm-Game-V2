namespace UIScripts
{
    using UnityEngine;
    using UnityEngine.UI;

    public static class ColorBlockCreator
    {
        public static ColorBlock SetHighlightedColor(ColorBlock _colorBlock, Color32 _highlightedColor, byte _alpha = 255)
        {
            _colorBlock.highlightedColor = new Color32(_highlightedColor.r, _highlightedColor.g, _highlightedColor.b, _alpha);
            return _colorBlock;
        }
    }
}