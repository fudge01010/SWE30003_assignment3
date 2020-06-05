using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class TakeAway : Order
    {
        private string name;
        public TakeAway(int customerId, string custName) : base(customerId)
        {

            name = custName;
        }

        public string Name()
        {
            return name;
        }
    }
}
