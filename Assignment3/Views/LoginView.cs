using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class LoginView : IView
    {
        private IController controller;
        private IView view;

        void IView.SetController(IController controller)
        {
            this.controller = controller;
        }

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
            Console.WriteLine(error);
        }
    }
}
