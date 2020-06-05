using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    class OrderController : IController
    {
        private OrderView view;

        public OrderController()
        {
            CreateView();
        }
        public string GetPrettyName()
        {
            return "Orders Page";
        }

        public void CreateView()
        {
            this.view = new OrderView();
        }

        void IController.SetView(IView view)
        {
            this.view = (OrderView)view;
        }

        void IController.Show()
        {
            Console.Clear();
        }
    }
}
