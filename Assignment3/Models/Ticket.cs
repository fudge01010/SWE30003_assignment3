using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Assignment3.Models
{
    class Ticket
    {
        private List<IItem> items;
        private DateTime timeOpened;
        private readonly Order parentOrder;

        public Ticket(Order parent, List<IItem> itemsOnTicket)
        {
            //constructor
            parentOrder = parent;
            items = itemsOnTicket;
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
            //parentOrder.TableNumber();
            return 0;
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

        
    }
}
