using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class SalesView : IView
    {
        public void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowView()
        {
            Console.Clear();
            Console.WriteLine(" ================= list of all Sales ==============");
            Console.WriteLine("");
            Console.WriteLine("| ID |" + " Sale Date".PadRight(29) + "| " + "price".PadRight(6) + "| " + "Customer".PadRight(20));
        }

        public void ShowSale(string sale)
        {
            //
            Console.WriteLine(sale);
        }
    }
}
