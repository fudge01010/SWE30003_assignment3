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
    }
}
