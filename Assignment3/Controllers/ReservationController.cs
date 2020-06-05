using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    class ReservationController : IController
    {
        private ReservationView view;
        private readonly List<Reservation> reservationList;
        public ReservationController()
        {
            // constructor
        }
        public void CreateView()
        {
            this.view = new ReservationView();
        }

        public string GetPrettyName()
        {
            return "Reservations Page";
        }

        public void SetView(IView view)
        {
            this.view = (ReservationView)view;
        }

        public void Show()
        {
            this.view.ShowView();

        }
    }
}
