using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class OrderView : IView
    {
        public void SetController(IController controller)
        {
            throw new NotImplementedException();
        }

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
            Console.WriteLine("  =============================== List of orders ===========================  ");
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void ShowOrder(int orderId, int tableNum, string openedFor, string items, float cost)
        {
            //
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Order ID:" + orderId.ToString());
            Console.WriteLine("Table number / customer ID:" + tableNum.ToString());
            Console.WriteLine("Opened for: " + openedFor);
            Console.WriteLine("Items on the order:");
            Console.WriteLine(items);
            Console.WriteLine("Total Order Cost:");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(cost.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowOrder(int tableNum, string openedFor, string items, float cost)
        {
            //
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Table number / customer ID:" + tableNum.ToString());
            Console.WriteLine("Opened for: " + openedFor);
            Console.WriteLine("Items on the order:");
            Console.WriteLine(items);
            Console.WriteLine("Total Order Cost:");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(cost.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
