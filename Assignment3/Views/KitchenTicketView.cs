using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class KitchenTicketView : IView
    {
        public KitchenTicketView()
        {
            // constructor 
        }

        public void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void ShowView()
        {
            throw new NotImplementedException();
        }
    }
}
