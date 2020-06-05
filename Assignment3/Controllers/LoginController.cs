using Assignment3.Helpers;
using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    class LoginController : IController
    {
        private IView loginView;
        private Login loginModel = new Login();
        public LoginController()
        {
            this.CreateView();
        }

        public void SetView(IView view)
        {
            this.loginView = view;
        }

        public void CreateView()
        {
            this.loginView = new LoginView();
        }

        public void Show()
        {
            this.loginView.ShowView();
            while (true)
            {
                string input = Console.ReadLine();
                Int16 pin;
                if (input.Length != 4)
                {
                    // needs to be 4 digits
                    this.loginView.ShowError("Please enter 4 digits.");
                } else if (!(Int16.TryParse(input, out(pin))))
                {
                    // cannot parse.
                    this.loginView.ShowError("please enter only numeric characters");
                } else
                {
                    //valid
                    if (!loginModel.checkLogin(pin))
                    {
                        this.loginView.ShowError("incorrect pin.");
                    } else
                    {
                        // authenticated
                        this.loginView.ShowMessage("authenticated.");
                        break;
                    }
                }
            }
            // authenticated, so lets show the menu.
            MenuHolder.GetMenuController().Show();
        }

        public string GetPrettyName()
        {
            return "Login Page";
        }
    }
}
