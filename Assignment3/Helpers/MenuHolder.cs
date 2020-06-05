using Assignment3.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Helpers
{
    public static class MenuHolder
    {
        private static MainMenuController menuController;

        public static MainMenuController GetMenuController()
        {
            return menuController;
        }

        public static void SetMenuController(MainMenuController contr)
        {
            menuController = contr;
        }
    }
}
