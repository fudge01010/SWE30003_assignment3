using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    class KitchenTicketController : IController
    {
        private readonly TicketManager ticketMan;

        private KitchenTicketView view;

        public KitchenTicketController(TicketManager ticman)
        {
            // constructor
            CreateView();
            ticketMan = ticman;
        }

        public TicketManager TicketManager()
        {
            return ticketMan;
        }

        public void CreateView()
        {
            this.view = new KitchenTicketView();
        }

        public string GetPrettyName()
        {
            return "Kitchen Tickets";
        }

        public void SetView(IView view)
        {
            this.view = (KitchenTicketView)view;
        }

        public void Show()
        {
            this.view.ShowView();
        }
    }
}
