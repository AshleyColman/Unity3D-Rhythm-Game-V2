namespace AccountScripts
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class AccountMenu : Menu
    {
        [SerializeField] private Signup signup = default;
        [SerializeField] private Login login = default;
        [SerializeField] private OptionMenu optionMenu = default;
        private Stack<Menu> menuStack = new Stack<Menu>();

        public void CancelButton_OnClick() => TransitionMenuBack();
        public void TransitionToMenu(Menu _menu)
        {
            menuStack.Push(_menu);
            _menu.TransitionIn();
        }
        protected override IEnumerator TransitionInCoroutine()
        {
            TransitionToMenu(optionMenu);
            return base.TransitionInCoroutine();
        }
        private void TransitionMenuBack()
        {
            var menu = menuStack.Pop();
            menu.TransitionOut();
            menu = menuStack.Peek();
            menu.TransitionIn();
        }
    }
}
