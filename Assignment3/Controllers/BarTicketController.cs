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
            if (ticketMan.BarTickets().Count == 0)
            {
                this.view.ShowMessage("There are no current bar tickets. Press enter to return to the main menu.\n\n");
                Console.ReadLine();
                MenuHolder.GetMenuController().Show();
                // should jump back to main menu here
            }
            for (i =0; i<ticketMan.BarTickets().Count; i++)
            {
                if (ticketMan.BarTickets()[i].Parent().GetType() == typeof(DineIn))
                {
                    // it's a dine in order
                    string items = "";
                    foreach (IItem itm in ticketMan.BarTickets()[i].BarItems())
                    {
                        items += "\t - " + itm.GetName() + "\n";
                    }
                    this.view.ShowTicket(ticketMan.BarTickets()[i].TableNumber(), items, (DateTime.Now- ticketMan.BarTickets()[i].TimeOpened()).ToString(), i);
                }
                if (ticketMan.BarTickets()[i].Parent().GetType() == typeof(TakeAway) )
                {
                    // it's a takeaway ticket
                    string items = "";
                    foreach (IItem itm in ticketMan.BarTickets()[i].BarItems())
                    {
                        items += "\t - " + itm.GetName() + "\n";
                    }
                    
                    this.view.ShowTicket(((TakeAway)ticketMan.BarTickets()[i].Parent()).Name(), items, (DateTime.Now - ticketMan.BarTickets()[i].TimeOpened()).ToString(), i);
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
                        //remove related items from ticket
                        foreach (IItem itm in ticketMan.BarTickets()[selection].BarItems())
                        {
                            ticketMan.BarTickets()[selection].Items().Remove(itm);
                        }
                        // ticket should now have only food items on it. If we've removed the last item from the ticket, close it fully:

                        if (ticketMan.BarTickets()[selection].Items().Count == 0)
                        {
                            // no items on ticket - delete ticket
                            ticketMan.BarTickets().Remove(ticketMan.BarTickets()[selection]);
                        }    
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
