using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    interface IItem
    {
        int GetId();
        string GetName();
        void SetName(string value);

        float GetPrice();
        void SetPrice(float value);

        string GetDescription();
        void SetDescription(string value);
    }
}
