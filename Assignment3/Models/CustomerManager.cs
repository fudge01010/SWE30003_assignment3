using Assignment3.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class CustomerManager
    {
        private readonly List<Customer> custs;
        public CustomerManager()
        {
            custs = new List<Customer>();
            // constructor
            custs.AddRange(DBManager.LoadFromDB_Customers());
        }

        public void AddCustomer(Customer customer)
        {
            custs.Add(customer);
        }

        public Customer GetCustomerByPhone(string phoneNumber)
        {
            //
            return custs.Find(x => x.GetPhone() == phoneNumber);
        }
    }
}
