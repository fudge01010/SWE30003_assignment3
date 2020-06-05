using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class Reservation
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

        public Reservation(int resId, int custId, DateTime resDateTime)
        {
            // constructor without table assosciated
            reservationId = resId;
            customerId = custId;
            reservationDate = resDateTime;
        }

    }
}
