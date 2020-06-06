using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Views
{
    class BarTicketView : IView
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
            Console.WriteLine("============ currently active BAR tickets: ============");
        }

        public void ShowTicket(int tableNumber, string items, string openFor, int ticketNumber)
        {
            // method for DineIn
            Console.WriteLine("Ticket Number: " + ticketNumber.ToString());
            Console.WriteLine("Ticket for table number: " + tableNumber.ToString());
            Console.WriteLine("Ticket has been open for: " + openFor);
            Console.WriteLine("Ticket contents:");
            Console.WriteLine(items);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public void ShowTicket(string name, string items, string openFor, int ticketNumber)
        {
            // overloaded method for takeaway (name instead of table number)
            Console.WriteLine("Ticket for takeaway order : " + name);
            Console.WriteLine("Ticket has been open for: " + openFor);
            Console.WriteLine("Ticket contents:");
            Console.WriteLine(items);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
    }
}
