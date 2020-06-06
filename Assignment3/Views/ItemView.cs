using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class ItemView : IView
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
            Console.WriteLine("  =============================== List of products ===========================  ");
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void ShowMenuItem(string identifier, string itemName, string desc, string price, string type)
        {
            Console.Write("| "+ identifier.PadRight(3) + " | ");
            Console.Write(itemName.PadRight(20) + " | ");
            Console.Write(desc.PadRight(50) + " | ");
            Console.Write(price.PadRight(5) + " | ");
            Console.WriteLine(type.PadRight(11) + "| ");
            //Console.WriteLine(itemName);
        }

    }
}
