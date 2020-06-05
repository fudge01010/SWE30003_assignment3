﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class Order
    {
        private DateTime timeOpened;
        private List<IItem> items;
        private int customerId;

        public Order()
        {
            //constructor
            timeOpened = DateTime.Now;
            items = new List<IItem>();
        }
    }
}
