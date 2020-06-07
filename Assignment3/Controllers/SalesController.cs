using Assignment3.Helpers;
using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    class SalesController : IController
    {
        public SalesView view;

        public SalesController()
        {
            CreateView();
        }
        public void CreateView()
        {
            this.view = new SalesView();
        }

        public string GetPrettyName()
        {
            return "View sales";
        }

        public void SetView(IView view)
        {
            this.view = (SalesView)view;
        }

        public void Show()
        {
            view.ShowView();
            // get the sales from the DB:
            List<string> sales = DBManager.GetSales();
            foreach (string s in sales)
            {
                view.ShowSale(s);
            }
            view.ShowMessage("Press enter to go back to the main menu...");
            Console.ReadLine();
            MenuHolder.GetMenuController().Show();
        }
    }
}
