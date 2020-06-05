using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public interface IController
    {
        public void SetView(IView view);
        public void CreateView();

        public void Show();

        public string GetPrettyName();
    }
}
