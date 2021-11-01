namespace UIScripts
{
    using UnityEngine;
    using UnityEngine.UI;

    public static class ColorBlockCreator
    {
        public static ColorBlock SetHighlightedColor(ColorBlock _colorBlock, Color32 _color, byte _alpha = 255)
        {
            _colorBlock.highlightedColor = new Color32(_color.r, _color.g, _color.b, _alpha);
            return _colorBlock;
        }
        public static ColorBlock SetSelectedColor(ColorBlock _colorBlock, Color32 _color, byte _alpha = 255)
        {
            _colorBlock.selectedColor = new Color32(_color.r, _color.g, _color.b, _alpha);
            return _colorBlock;
        }
    }
}