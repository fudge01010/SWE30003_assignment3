using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    class ItemController : IController
    {
        private ItemView view;
        private readonly ItemManager itemManager;
        public ItemController(ItemManager manager)
        {
            // constructor
            itemManager = manager;
            CreateView();
        }
        public void CreateView()
        {
            this.view = new ItemView();
        }

        public string GetPrettyName()
        {
            return "Items page";
        }

        public void SetView(IView view)
        {
            this.view = (ItemView)view;
        }

        public void Show()
        {
            view.ShowView();
            view.ShowMenuItem("ID", "Name", "Description", "Price", "Type");
            foreach (IItem item in itemManager.GetItems())
            {
                view.ShowMenuItem(item.GetId().ToString(), item.GetName(), item.GetDescription(), item.GetPrice().ToString(), item.ToString());
            }
            while (true)
            {
                view.ShowMessage("");
                break;
            }
        }
    }
}
