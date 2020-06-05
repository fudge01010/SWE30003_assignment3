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

        public Ticket(Order parent, List<IItem> itemsOnTicket)
        {
            //constructor
            parentOrder = parent;
            items = itemsOnTicket;
        }

        public Order Parent()
        {
            return parentOrder;
        }
        public int TableNumber()
        {
            //returns tableNumber, based on parent order
            //parentOrder.TableNumber();
            return 0;
        }
    }
}
