using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class DineIn : Order
    {
        private readonly int tabNum;
        public DineIn(int customerId, int tableNumber) : base(customerId)
        {
            // constructor
            tabNum = tableNumber;
        }

        public override int TableNumber()
        {
            return tabNum;
        }
    }
}
