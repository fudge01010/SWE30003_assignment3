using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public class Customer
    {
        private int customerID;
        private string customerName;
        private string customerPhone;
        private string customerAddress;
        private DateTime customerDateCreated;


        public Customer(int id, string name, string phone, string address, DateTime createdDate)
        {
            // constructor
            customerID = id;
            if (name == null)
            {
                customerName = "";
            }
            else
            {
                customerName = name;
            }
            if (phone == null)
            {
                customerPhone = "";
            }
            else
            {
                customerPhone = phone;
            }
            if (address == null)
            {
                customerAddress = "";
            }
            else
            {
                customerAddress = address;
            }
            customerDateCreated = createdDate;
        }

        public string GetPhone()
        {
            return customerPhone;
        }

        public string GetAddress()
        {
            return customerAddress;
        }

        public string GetName()
        {
            return customerName;
        }

        public int GetId()
        {
            return customerID;
        }
    }
}
