using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class LoginView : IView
    {
        private IView view;

        public void ShowView()
        {
            Console.Clear();
            Console.WriteLine("Enter your pin number to login");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
