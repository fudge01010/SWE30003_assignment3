using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    interface IItem
    {
        int GetId();
        string GetName();

        float GetPrice();

        string GetDescription();
    }
}
