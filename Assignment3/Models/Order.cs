using System;
using System.Collections.Generic;

namespace Assignment3.Models
{
    class Order
    {
        private DateTime timeOpened;
        private List<IItem> items;
        private int customerId;

        public Order(int custId)
        {
            //constructor
            timeOpened = DateTime.Now;
            items = new List<IItem>();
            customerId = custId;
        }

        public void AddItems(List<IItem> itemsToAdd)
        {
            items.AddRange(itemsToAdd);
        }
        
        public int CustomerID()
        {
            return customerId;
        }


    }
}
