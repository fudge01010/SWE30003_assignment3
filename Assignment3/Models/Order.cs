using System;
using System.Collections.Generic;

namespace Assignment3.Models
{
    class Order
    {
        private DateTime timeOpened;
        private List<IItem> items;
        private int customerId;
        private List<Ticket> tickets;
        private Ticket tempTicket;
        public Order(int custId)
        {
            //constructor
            timeOpened = DateTime.Now;
            items = new List<IItem>();
            customerId = custId;
            tickets = new List<Ticket>();
        }

        public void AddItems(List<IItem> itemsToAdd)
        {
            items.AddRange(itemsToAdd);
        }
        
        public int CustomerID()
        {
            return customerId;
        }

        public string OpenedFor()
        {
            return (DateTime.Now - timeOpened).ToString();
        }

        public int NumberOfItems()
        {
            return items.Count;
        }

        public virtual int TableNumber()
        {
            // base virtual to be overloaded by inheriting classes
            return 0;
        }

        public List<IItem> ItemsOnOrder()
        {
            // returns a list of all the items on the order.
            return items;
        }

        public List<IItem> ItemsOnOrderTemp()
        {
            List<IItem> items = new List<IItem>();
            items.AddRange(ItemsOnOrder());
            items.AddRange(tempTicket.Items());
            return items;
        }

        public string FormattedItemsOnOrder()
        {
            string items = "";
            foreach (IItem item in ItemsOnOrder())
            {
                items += "\t- " + item.GetName() + "\n";
            }
            return items;
        }

        public float GetOrderCost()
        {
            float cost = 0.0f;
            foreach (IItem i in ItemsOnOrder())
            {
                cost += i.GetPrice();
            }
            return (float)Math.Round(cost, 2);
        }

        public List<Ticket> GetTickets()
        {
            return tickets;
        }

        public Ticket TempTicket()
        {
            return tempTicket;
        }

        public void SetTempTicket(Ticket ticket)
        {
            tempTicket = ticket;
        }


    }
}
