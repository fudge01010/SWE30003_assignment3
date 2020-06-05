using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public interface IView
    {

        public void ShowView();

        public void ShowError(string error);
        public void ShowMessage(string message);
    }
}
