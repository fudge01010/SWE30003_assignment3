using Assignment3.Helpers;
using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Controllers
{
    class BarTicketController : IController
    {
        private BarTicketView view;

        private readonly TicketManager ticketMan;

        public BarTicketController(TicketManager ticman)
        {
            CreateView();
            ticketMan = ticman;
        }
        public void CreateView()
        {
            this.view = new BarTicketView();
        }

        public string GetPrettyName()
        {
            return "Bar Tickets";
        }

        public void SetView(IView view)
        {
            this.view = (BarTicketView)view;
        }

        public void Show()
        {
            this.view.ShowView();
            int i = 0;
            for (i =0; i<ticketMan.Tickets().Count; i++)
            {
                if (ticketMan.Tickets()[i].Parent().GetType() == typeof(DineIn))
                {
                    // it's a dine in order
                    string items = "";
                    foreach (IItem itm in ticketMan.Tickets()[i].BarItems())
                    {
                        items += "\t - " + itm.GetName() + "\n";
                    }
                    this.view.ShowTicket(ticketMan.Tickets()[i].TableNumber(), items, (DateTime.Now- ticketMan.Tickets()[i].TimeOpened()).ToString(), i);
                }
                if (ticketMan.Tickets()[i].Parent().GetType() == typeof(TakeAway) )
                {
                    // it's a takeaway ticket
                    string items = "";
                    foreach (IItem itm in ticketMan.Tickets()[i].BarItems())
                    {
                        items += "\t - " + itm.GetName() + "\n";
                    }
                    
                    this.view.ShowTicket(((TakeAway)ticketMan.Tickets()[i].Parent()).Name(), items, (DateTime.Now - ticketMan.Tickets()[i].TimeOpened()).ToString(), i);
                }
                i++;
            }
            this.view.ShowMessage("Enter a ticket number to mark it as complete.\nEnter 'm' to go back to the menu.");
            while (true)
            {
                
                string input = Console.ReadLine();
                int selection;
                if (!Int32.TryParse(input, out(selection)))
                {
                    if (input != "m")
                    {
                        this.view.ShowError("Enter a valid option");
                        continue;
                    } else
                    {
                        // m is entered - go back to the menu.
                        MenuHolder.GetMenuController().Show();
                    }
                } else
                {
                    if (selection > i || selection < 0)
                    {
                        this.view.ShowError("Enter a valid option");
                        continue;
                    } else
                    {
                        ticketMan.Tickets().Remove(ticketMan.Tickets()[selection]);
                        // Business logic to mark ticket as complete, and fold items into order.
                        // markoff[input] - or something to that effect
                        Show();
                    }
                }
            }
        }

        public TicketManager TicketManager()
        {
            return ticketMan;
        }
    }
}
