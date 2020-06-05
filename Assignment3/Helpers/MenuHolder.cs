using Assignment3.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Helpers
{
    public static class MenuHolder
    {
        private static MenuController menuController;

        public static MenuController GetMenuController()
        {
            return menuController;
        }

        public static void SetMenuController(MenuController contr)
        {
            menuController = contr;
        }
    }
}
