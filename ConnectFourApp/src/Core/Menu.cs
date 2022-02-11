using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Core
{
    class MenuItem
    {
        public string[] choices;
        public int[] linkedMenu;
        public string title;

        public MenuItem(string[] choices, int[] linkedMenu, string title)
        {
            this.choices = choices;
            this.linkedMenu = linkedMenu;
            this.title = title;
        }

        public static MenuItem[] CreateAllMenu()
        {
            List<MenuItem> menuList = new List<MenuItem>();
            menuList.Add(new MenuItem(new string[] { "Register", "Login", "Play", "Statistic", "Exit" }, new int[] {1,2,3,4,5},"Connect Four"));
            menuList.Add(new MenuItem(new string[] { "Enter Username", "Enter Passwort", "Submit" }, new int[] { -1, -1, -2 }, "Register?"));
            menuList.Add(new MenuItem(new string[] { "Enter Username", "Enter Passwort"}, new int[] { -1, -1 }, "Login?"));
            menuList.Add(new MenuItem(new string[] { "Yes", "No" }, new int[] { -1, -2}, "Play"));
            menuList.Add(new MenuItem(new string[] { "Yes", "No" }, new int[] { -1, -2}, "Statistic"));
            menuList.Add(new MenuItem(new string[] { "Yes", "No" }, new int[] { -1, -2}, "Exit?"));

            return menuList.ToArray();
        }
    }

    class Menu
    {
        private static readonly Menu instance = new Menu();
        public MenuItem[] menu;

        private Menu() {
            menu = MenuItem.CreateAllMenu();
        }

        public static Menu GetInstance() {
            return instance;
        }
    }
}
