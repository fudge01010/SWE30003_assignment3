using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class DineIn : Order
    {
        private readonly int tableNumber;
        public DineIn(int table)
        {
            // constructor
            tableNumber = table;
        }

        public int TableNumber()
        {
            return tableNumber;
        }
    }
}
