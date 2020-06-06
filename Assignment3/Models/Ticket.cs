using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment3.Models
{
    class Ticket
    {
        private List<IItem> items;
        private DateTime timeOpened;
        private readonly Order parentOrder;

        public Ticket(Order parent)
        {
            //constructor
            parentOrder = parent;
            items = new List<IItem>();
            timeOpened = DateTime.Now;
        }

        public Order Parent()
        {
            return parentOrder;
        }

        public DateTime TimeOpened()
        {
            return timeOpened;
        }

        public int TableNumber()
        {
            //returns tableNumber, based on parent order
            return parentOrder.TableNumber();
        }

        public List<FoodItem> KitchenItems()
        {
            List<FoodItem> foodItems = (items.FindAll(x => x.GetType() == typeof(FoodItem))).Cast<FoodItem>().ToList();
            return foodItems;
        }

        public List<DrinkItem> BarItems()
        {
            List<DrinkItem> drinkItems = (items.FindAll(x => x.GetType() == typeof(DrinkItem))).Cast<DrinkItem>().ToList();
            return drinkItems;
        }

        public List<IItem> Items()
        {
            return items;
        }

        public void MarkTicketComplete()
        {
            // first, fold the items from the ticket's list into the orders list.
            items.Clear();
        }

        public void AddItem(IItem item)
        {
            items.Add(item);
        }
        
    }
}
