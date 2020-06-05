using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class Ticket
    {
        private List<IItem> items;
        private DateTime timeOpened;
        private readonly Order parentOrder;

        public Ticket(Order parent)
        {
            //constructor
            parentOrder = parent;
        }
    }
}
