using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class MenuView : IView
    {

        private IController controller;
        public void SetController(IController controller)
        {
            this.controller = controller;
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
            Console.WriteLine("========== Select a menu option ========");
        }

        public void ShowMenuItem(string identifier, string itemName)
        {
            Console.Write(identifier);
            Console.Write("\t");
            Console.WriteLine(itemName);
        }

        public void ShowMenuItem(int identifier, string itemName)
        {
            ShowMenuItem(identifier.ToString(), itemName);
        }
    }
}
