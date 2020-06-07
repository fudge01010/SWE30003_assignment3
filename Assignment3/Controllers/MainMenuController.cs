using Assignment3.Helpers;
using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    public class MainMenuController : IController
    {
        private MenuView view;

        private List<IController> controllerList = new List<IController>();
        public MainMenuController()
        {
            // constructor
            CreateView();
        }
        public void CreateView()
        {
            this.view = new MenuView();   
        }

        public void SetView(IView view)
        {
            this.view = (MenuView)view;
        }

        public void Show()
        {
            this.view.ShowView();
            for (int i = 0; i < controllerList.Count; i++)
            {
                this.view.ShowMenuItem(i, controllerList[i].GetPrettyName());
            }
            this.view.ShowMenuItem(controllerList.Count, "Exit System");
            this.view.ShowMessage("enter the number of your page selection.");
            while (true)
            {
                
                string sel = Console.ReadLine();
                Int16 option;
                if (!Int16.TryParse(sel, out (option)))
                {
                    this.view.ShowError("Please enter a valid number.");
                    continue;
                }
                else if (option > controllerList.Count || option < 0)
                {
                    this.view.ShowError("Please enter a number within the range specified.");
                    continue;                }
                else if (option == controllerList.Count)
                {
                    //exiting the system
                    DBManager.CloseConnection();
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                } else
                {
                    // valid selection - go to it.
                    controllerList[option].Show();
                }
            }
        }

        public void AddController(IController toAdd)
        {
            // allows a controller to be added to the List
            this.controllerList.Add(toAdd);
        }

        public string GetPrettyName()
        {
            return "Menu Page";
        }
    }
}
