using Assignment3.Models;
using Assignment3.Views;
using Assignment3.Controllers;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Assignment3.Helpers;

namespace Assignment3
{
    class Program
    {
        protected IController loginController;
        protected IController orderController;
        static protected MenuController menuController;

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Program program = new Program();
            program.SetupProgram();
        }

        public void SetupProgram()
        {
            // create menu controller, which holds a list of other controllers to generate menu.
            menuController = new MenuController();
            //add a reference to that controller to the static helper MenuHolder function.
            MenuHolder.SetMenuController(menuController);

            loginController = new LoginController();
            menuController.AddController(loginController);

            orderController = new OrderController();
            menuController.AddController(orderController);

            // go to the login/auth page
            loginController.Show();
        }

        public IController getMenuController()
        {
            return menuController;
        }
    }
}
