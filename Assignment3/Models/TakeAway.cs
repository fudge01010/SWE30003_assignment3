using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class TakeAway : Order
    {
        private string name;
        private int custId;
        public TakeAway(int customerId, string custName) : base(customerId)
        {

            name = custName;
            custId = customerId;
        }

        public string Name()
        {
            return name;
        }

        public override int TableNumber()
        {
            return custId;
        }
    }
}
