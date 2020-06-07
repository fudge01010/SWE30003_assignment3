using Assignment3.Helpers;
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
        private OrderController orderController;
        public ReservationController(OrderController ordercon)
        {
            // constructor
            CreateView();
            reservationList = DBManager.LoadFromDB_Reservations();
            orderController = ordercon;   
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
            view.ShowReservation("| " + "id".PadRight(4) + "| " + "Customer Name".PadRight(20) + "| " + "table".PadRight(5) + "| " + "reservation date/time".PadRight(25) + "|");
            foreach (Reservation res in reservationList)
            {
                // uses ToString overload of res to return formatted details
                view.ShowReservation(res.ToString());
            }
            view.ShowMessage("Enter 'm' to go to main menu, 'a' to add a new reservation, or 'c' to check in a reservation");
            string input = "";
            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "m":
                        MenuHolder.GetMenuController().Show();
                        break;
                    case "a":
                        view.ShowMessage("Enter the customers phone number to make a reservation");
                        int custId = DBManager.LookupCustomer(Console.ReadLine());
                        view.ShowMessage("Enter the reservation, in the format 'yyyy-MM-dd hh:mm:ss");
                        DateTime reqTime = DateTime.Parse(Console.ReadLine());
                        view.ShowMessage("Enter the table number");
                        int tableNum = int.Parse(Console.ReadLine());
                        Reservation newRes = new Reservation(DBManager.GetNextResId(), custId, tableNum, reqTime);
                        DBManager.AddReservation(newRes);
                        reservationList.Add(newRes);
                        Console.WriteLine("Reservation added. Press enter to refresh...");
                        Console.ReadLine();
                        Show();
                        break;
                    case "c":
                        view.ShowMessage("Enter the ID from the table above, to check the reservation in.");
                        int resId = int.Parse(Console.ReadLine());
                        Reservation theRes = DBManager.GetReservation(resId);
                        orderController.NewDineInOrder(DBManager.GetCustomer(theRes.GetCustId()), DBManager.GetTable(theRes.GetTableId()));

                        break;
                    default:
                        view.ShowError("Please enter either 'm', 'a' or 'c'");
                        break;
                }
            }
            

        }
    }
}
