using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class FoodItem : IItem
    {
        private string name;
        private string description;
        private float price;
        private readonly int id;

        public FoodItem(int itemid, string itemname, string descri, float itemprice)
        {
            // constructor
            id = itemid;
            name = itemname;
            description = descri;
            price = itemprice;
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
    }
}
