using Assignment3.Helpers;
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
            view.ShowMessage("Enter 'm' to go back to the menu. Enter 'e' to edit an item. Enter 'c' to create a new item.");
            while (true)
            {
                
                string input = Console.ReadLine();
                switch (input)
                {
                    case "m":
                        MenuHolder.GetMenuController().Show();
                        break;
                    case "e":
                        view.ShowMessage("enter the item ID to edit");
                        string inc = Console.ReadLine();
                        int id = 0;
                        bool status = int.TryParse(inc, out (id));
                        while (!status || (id < 0 || id > itemManager.GetItems().Count))
                        {
                            view.ShowError("please enter a valid number");
                        }
                        EditItem(id);
                        break;
                    case "c":
                        NewItem();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid command.");
                        break;
                }
            }
        }
        private void NewItem()
        {
            view.Clear();
            view.ShowMessage("Enter the product name:");
            string prodname = Console.ReadLine();
            while (prodname.Length < 2)
            {
                view.ShowError("Please enter a suitable length name");
                prodname = Console.ReadLine();
            }
            view.ShowMessage("Please enter the product description");
            string proddesc = Console.ReadLine();
            while (proddesc.Length < 5)
            {
                view.ShowError("Please enter a suitable length description");
            }
            view.ShowMessage("Please enter the product price");
            string prodprice = Console.ReadLine();
            float price;
            while (!float.TryParse(prodprice, out(price)))
            {
                view.ShowError("Please enter a valid price.");
                prodprice = Console.ReadLine();
            }
            view.ShowMessage("Please enter 0 for food (kitchen) item, or 1 for drink (bar) item:");
            string prodtype = Console.ReadLine();
            int itemType;
            bool status = int.TryParse(prodtype, out (itemType));
            while (status == false || !(itemType == 1 || itemType == 0))
            {
                view.ShowError("Please enter a valid option.");
                prodtype = Console.ReadLine();
                status = int.TryParse(prodtype, out (itemType));
            }
            // should have valid product specs. Lets create (and write):
            // get next valid item ID from the DB:
            int itemId = DBManager.GetNextProductId();
            if (itemType == 1)
            {
                // drink item
                
                DrinkItem createdDrink = new DrinkItem(itemId, prodname, proddesc, price);
                itemManager.AddItem(createdDrink);
                DBManager.AddProduct(createdDrink);
            } else if (itemType == 0)
            {
                // food item
                FoodItem createdFood = new FoodItem(itemId, prodname, proddesc, price);
                itemManager.AddItem(createdFood);
                DBManager.AddProduct(createdFood);
            } else
            {
                // shouldn't get here.
                throw new ArgumentOutOfRangeException();
            }
            Show();
        }

        public void EditItem(int idToEdit)
        {
            view.Clear();
            view.ShowMenuItem(idToEdit.ToString(), itemManager.GetItem(idToEdit).GetName(), itemManager.GetItem(idToEdit).GetDescription(), itemManager.GetItem(idToEdit).GetPrice().ToString(), itemManager.GetItem(idToEdit).ToString());
            view.ShowMessage("Enter the updated product name:");
            string prodname = Console.ReadLine();
            while (prodname.Length < 2)
            {
                view.ShowError("Please enter a suitable length name");
                prodname = Console.ReadLine();
            }
            view.ShowMessage("Please enter the updated product description");
            string proddesc = Console.ReadLine();
            while (proddesc.Length < 5)
            {
                view.ShowError("Please enter a suitable length description");
            }
            view.ShowMessage("Please enter the updated product price");
            string prodprice = Console.ReadLine();
            float price;
            while (!float.TryParse(prodprice, out (price)))
            {
                view.ShowError("Please enter a valid price.");
                prodprice = Console.ReadLine();
            }

            // should have valid product specs. Lets update product (and write):
            // get next valid item ID from the DB:
            itemManager.GetItem(idToEdit).UpdatePrice(price);
            itemManager.GetItem(idToEdit).UpdateName(prodname);
            itemManager.GetItem(idToEdit).UpdateDescription(proddesc);

            DBManager.UpdateProduct(itemManager.GetItem(idToEdit));
            Show();
        }
    }
}
