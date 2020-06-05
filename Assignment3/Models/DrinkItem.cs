using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class DrinkItem : IItem
    {
        private string name;
        private string description;
        private float price;
        private readonly int id;

        public DrinkItem(int itemid)
        {
            id = itemid;
        }
        public string GetName()
        {
            return name;
        }

        public void SetName(string value)
        {
            name = value;
        }

        public float GetPrice()
        {
            return price;
        }

        public void SetPrice(float value)
        {
            price = value;
        }

        public string GetDescription()
        {
            return description;
        }

        public void SetDescription(string value)
        {
            description = value;
        }

        public int GetId()
        {
            return id;
        }
    }
}
