using Assignment3.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public class Reservation
    {
        private DateTime reservationDate;
        private readonly int reservationId;
        private readonly int customerId;
        private readonly int tableId;
        private readonly Table table;
        public Reservation(int resId, int custId, int tId, DateTime resDateTime)
        {
            //constructor
            reservationId = resId;
            customerId = custId;
            tableId = tId;
            reservationDate = resDateTime;
        }

        public Reservation (int resId, int custId, Table table, DateTime resDateTime)
        {
            // another constructor, with the table instead of tableID
            reservationId = resId;
            customerId = custId;
            tableId = table.GetID();
            reservationDate = resDateTime;
        }

        public Reservation(int resId, int custId, DateTime resDateTime)
        {
            // constructor without table assosciated
            reservationId = resId;
            customerId = custId;
            reservationDate = resDateTime;
        }

        public int GetId()
        {
            return reservationId;
        }

        public int GetCustId()
        {
            return customerId;
        }

        public int GetTableId()
        {
            return tableId;
        }

        public DateTime GetReservationTime()
        {
            return reservationDate;
        }

        public override string ToString()
        { 
            string ret = "| " + reservationId.ToString().PadRight(4) + "| " + DBManager.LookupCustomerName(customerId).PadRight(20) + "| " + tableId.ToString().PadRight(5) + "| " + reservationDate.ToString().PadRight(25) + "|";
            return ret;
        }
    }
}
