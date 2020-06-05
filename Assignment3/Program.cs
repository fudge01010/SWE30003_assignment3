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
        protected TableManager tableManager;
        protected CustomerManager customerManager;
        static protected MainMenuController menuController;

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Program program = new Program();
            program.SetupProgram();
        }

        public void SetupProgram()
        {
            // create the DBManager static helper, and let it create the SQLite connection.
            DBManager.CreateConnection();

            // create menu controller, which holds a list of other controllers to generate menu.
            menuController = new MainMenuController();
            //add a reference to that controller to the static helper MenuHolder function.
            MenuHolder.SetMenuController(menuController);

            // add the remaining screen controllers to the main menu controller.
            loginController = new LoginController();
            menuController.AddController(loginController);

            orderController = new OrderController();
            menuController.AddController(orderController);

            // now we have all the mvc controllers created, create the "managers" for the data models.
            
            tableManager = new TableManager();

            customerManager = new CustomerManager();






            // load the contents from the 
            DBManager.CloseConnection();

            // go to the login/auth page
            loginController.Show();
        }

        public IController getMenuController()
        {
            return menuController;
        }
    }
}
