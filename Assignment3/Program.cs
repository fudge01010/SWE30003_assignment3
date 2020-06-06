using Assignment3.Models;
using Assignment3.Controllers;
using System;
using Assignment3.Helpers;

namespace Assignment3
{
    class Program
    {
        // MVC controllers
        protected IController loginController;
        protected IController orderController;
        protected IController kitchenTicketController;
        protected IController barTicketController;
        protected IController itemController;

        // "managers" - in the Model domain
        protected TableManager tableManager;
        protected CustomerManager customerManager;
        protected TicketManager ticketManager;
        protected ItemManager itemManager;
        

        // a special main menu controller (static) so we can always find our way home :)
        static protected MainMenuController menuController;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("Hello World!");
            Program program = new Program();
            program.SetupProgram();
        }

        public void SetupProgram()
        {
            // this is the "bootstrap" method, you could say.... ;)

            // create the DBManager static helper, and let it create the SQLite connection.
            DBManager.CreateConnection();

            // create menu controller, which holds a list of other controllers to generate menu.
            menuController = new MainMenuController();
            //add a reference to that controller to the static helper MenuHolder function.
            MenuHolder.SetMenuController(menuController);

            // create the login controller screen
            loginController = new LoginController();
            menuController.AddController(loginController);

            // create the table manager (loads tables from DB upon creation)
            tableManager = new TableManager();

            // create the item manager (loads items from DB upon creation)
            itemManager = new ItemManager();

            // create the TicketManager before creating the ticket controllers, so we can pass that in via reference
            ticketManager = new TicketManager();

            // create the order controller screen
            orderController = new OrderController(tableManager, itemManager, ticketManager);
            menuController.AddController(orderController);

            kitchenTicketController = new KitchenTicketController(ticketManager);
            menuController.AddController(kitchenTicketController);

            barTicketController = new BarTicketController(ticketManager);
            menuController.AddController(barTicketController);

            // create the item manager, and then the item view controller. Pass manager into controller.

            itemController = new ItemController(itemManager);
            menuController.AddController(itemController);

            // now we have all the mvc controllers created, create the "managers" for the data models.
            // each constructor should have a call to the DB helper class, to load it's data.
            
            

            customerManager = new CustomerManager();

            

            

            






            // load the contents from the 

            // go to the login/auth page
            loginController.Show();
        }

        public IController getMenuController()
        {
            return menuController;
        }
    }
}
