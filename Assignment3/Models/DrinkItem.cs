using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public class DrinkItem : IItem
    {
        private string name;
        private string description;
        private float price;
        private readonly int id;

        public DrinkItem(int itemid)
        {
            id = itemid;
        }

        public DrinkItem(int itemid, string itemname, string itemdescription, float itemprice)
        {
            // overloaded constructor
            id = itemid;
            description = itemdescription;
            price = itemprice;
            name = itemname;
        }
        public string GetName()
        {
            return name;
        }


        public float GetPrice()
        {
            return price;
        }


        public string GetDescription()
        {
            return description;
        }


        public int GetId()
        {
            return id;
        }

        public override string ToString()
        {
            return "Drink Item";
        }

        public void UpdateName(string newName)
        {
            name = newName;
        }

        public void UpdatePrice(float newPrice)
        {
            price = newPrice;
        }

        public void UpdateDescription(string newDesc)
        {
            description = newDesc;
        }
    }
}
