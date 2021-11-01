namespace ListMenuScripts
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using System.Text;
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using StaticDataScripts;

    public sealed class ButtonListFilter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField searchInputField = default;
        [SerializeField] private Toggle toggle1C = default;
        [SerializeField] private Toggle toggle2C = default;
        [SerializeField] private Toggle toggle3C = default;
        [SerializeField] private Toggle toggle4C = default;
        [SerializeField] private Toggle toggle5C = default;
        [SerializeField] private Toggle toggle6C = default;
        [SerializeField] private Button clearButton = default;
        [SerializeField] private ButtonList buttonList = default;
        private List<BeatmapButton> activeButtonList = new List<BeatmapButton>();
        private StringBuilder searchStringBuilder = new StringBuilder();
        private IEnumerator searchCoroutine;

        public void Search()
        {
            if (searchCoroutine != null)
            {
                StopCoroutine(searchCoroutine);
            }
            searchCoroutine = SearchCoroutine();
            StartCoroutine(searchCoroutine);
        }
        private IEnumerator SearchCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(0.05f);
            ClearActiveButtonList();
            buttonList.DeactivateContent();
            yield return wait;
            foreach (var button in buttonList.BeatmapButtonList)
            {
                // Remove all buttons that do not match the toggled on Modes, before sorting via string.
                bool isRemoved = FilterModeToggle(toggle1C, button, Mode.One);
                if (isRemoved == true) { continue; }
                isRemoved = FilterModeToggle(toggle2C, button, Mode.Two);
                if (isRemoved == true) { continue; }
                isRemoved = FilterModeToggle(toggle3C, button, Mode.Three);
                if (isRemoved == true) { continue; }
                isRemoved = FilterModeToggle(toggle4C, button, Mode.Four);
                if (isRemoved == true) { continue; }
                isRemoved = FilterModeToggle(toggle5C, button, Mode.Five);
                if (isRemoved == true) { continue; }
                isRemoved = FilterModeToggle(toggle6C, button, Mode.Six);
                if (isRemoved == true) { continue; }

                searchStringBuilder.Clear();
                searchStringBuilder.Append(button.NumberText.text);
                searchStringBuilder.Append(button.TitleText.text);
                searchStringBuilder.Append(button.ArtistText.text);
                searchStringBuilder.Append(button.CreatorText.text);
                searchStringBuilder.Append(button.DateText.text);
                searchStringBuilder.Append(button.ModeText.text);
                if (searchStringBuilder.ToString().Contains(searchInputField.text, StringComparison.OrdinalIgnoreCase))
                {
                    button.Activate();
                    AddToActiveButtonList(button);
                    buttonList.SetButtonTransformToActive(button);
                }
                else
                {
                    button.Deactivate();
                    buttonList.SetButtonTransformToDisabled(button);
                }
                yield return wait;
            }
            yield return wait;
            SetActiveButtonListNumberText();
            yield return wait;
            buttonList.ActivateList();
            yield return null;
        }

        private bool FilterModeToggle(Toggle _toggle, BeatmapButton _button, Mode _mode)
        {
            if (_toggle.isOn)
            {
                if (_button.Mode != _mode)
                {
                    _button.Deactivate();
                    buttonList.SetButtonTransformToDisabled(_button);
                    return false;
                }
            }
            return true;
        }

        //private IEnumerator SearchCoroutine()
        //{
        //    WaitForSeconds wait = new WaitForSeconds(0.05f);
        //    ClearActiveButtonList();
        //    buttonList.DeactivateContent();
        //    yield return wait;
        //    foreach (var button in buttonList.BeatmapButtonList)
        //    {
        //        searchStringBuilder.Clear();
        //        searchStringBuilder.Append(button.NumberText.text);
        //        searchStringBuilder.Append(button.TitleText.text);
        //        searchStringBuilder.Append(button.ArtistText.text);
        //        searchStringBuilder.Append(button.CreatorText.text);
        //        searchStringBuilder.Append(button.DateText.text);
        //        searchStringBuilder.Append(button.ModeText.text);
        //        if (searchStringBuilder.ToString().Contains(searchInputField.text, StringComparison.OrdinalIgnoreCase))
        //        {
        //            button.Activate();
        //            AddToActiveButtonList(button);
        //            buttonList.SetButtonTransformToActive(button);
        //        }
        //        else
        //        {
        //            button.Deactivate();
        //            buttonList.SetButtonTransformToDisabled(button);
        //        }
        //        yield return wait;
        //    }
        //    yield return wait;
        //    SetActiveButtonListNumberText();
        //    yield return wait;
        //    buttonList.ActivateList();
        //    yield return null;
        //}
        private void ClearActiveButtonList() => activeButtonList.Clear();
        private void AddToActiveButtonList(BeatmapButton _button) => activeButtonList.Add(_button);
        private void SetActiveButtonListNumberText()
        {
            int index = 0;
            foreach (var button in activeButtonList)
            {
                button.SetButtonNumberText((index + 1).ToString());
                index++;
            }
        }
    }
}