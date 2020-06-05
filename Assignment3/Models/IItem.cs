using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public interface IItem
    {
        int GetId();
        string GetName();

        float GetPrice();

        string GetDescription();
    }
}
