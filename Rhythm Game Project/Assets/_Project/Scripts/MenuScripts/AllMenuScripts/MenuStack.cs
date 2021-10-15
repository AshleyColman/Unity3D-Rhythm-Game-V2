namespace AllMenuScripts
{
    using StartMenuScripts;
    using System.Collections.Generic;
    using UnityEngine;

    public class MenuStack : MonoBehaviour
    {
        [SerializeField] private Menu onAwakeMenu = default;
        private Stack<Menu> menuStack = new Stack<Menu>();

        public void ClearMenuStack() => menuStack.Clear();
        public void TransitionToMenu(Menu _menu)
        {
            menuStack.Push(_menu);
            _menu.TransitionIn();
        }
        public void TransitionMenuBack()
        {
            var menu = menuStack.Pop();
            menu.TransitionOut();
            menu = menuStack.Peek();
            menu.TransitionIn();
        }
        private void Awake()
        {
            if (onAwakeMenu == null)
            {
                onAwakeMenu = FindObjectOfType<StartMenuManager>();
            }
            TransitionToMenu(onAwakeMenu);
        }
    }
}