using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3
{
    class Login
    {
        public Login()
        {
            // constructor method
        }

        public bool checkLogin(Int16 pinCode)
        {
            if (pinCode == 1234)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
