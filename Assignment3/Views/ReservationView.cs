using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class ReservationView : IView
    {
        public void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowView()
        {
            Console.Clear();
            Console.WriteLine(" ================= list of all reservations ==============");
        }

        public void ShowReservation()
        {
            //
        }
    }
}
