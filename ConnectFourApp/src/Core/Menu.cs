using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Core
{
    class Menu
    {
        public string[] choices;
        public int[] linkedMenu;
        public string title;
        public int selected;
        public int previousMenu;

        public Menu(string[] choices, int[] linkedMenu, string title, int selected)
        {
            this.choices = choices;
            this.linkedMenu = linkedMenu;
            this.title = title;
            this.selected = selected;
        }

        public static Menu[] CreateAllMenu()
        {
            List<Menu> menuList = new List<Menu>();
            menuList.Add(new Menu(new string[] { "Register", "Login", "Play", "Exit" }, new int[] {1,2,3,4},"Connect Four",0));
            menuList.Add(new Menu(new string[] { "Enter Username", "Enter Passwort", "Enter Email", "Submit" }, new int[] { -1, -1, -1, -2 }, "Register?", 0));
            menuList.Add(new Menu(new string[] { "Enter Username", "Enter Passwort", "Submit" }, new int[] { -1, -1, -2 }, "Login?", 0));
            menuList.Add(new Menu(new string[] { "Yes", "No" }, new int[] { -1, -1}, "Exit?", 0));

            return menuList.ToArray();
        }
    }
}
