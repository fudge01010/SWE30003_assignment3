using Assignment3.Helpers;
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
            int i = 0;
            foreach (Ticket t in ticketMan.Tickets())
            {
                if (t.Parent().GetType() == typeof(DineIn))
                {
                    // it's a dine in order
                    string items = "";
                    foreach (IItem itm in t.KitchenItems())
                    {
                        items += "\t - " + itm.GetName() + "\n";
                    }
                    this.view.ShowTicket(t.TableNumber(), items, (DateTime.Now - t.TimeOpened()).ToString(), i);
                }
                if (t.Parent().GetType() == typeof(TakeAway))
                {
                    // it's a takeaway ticket
                    string items = "";
                    foreach (IItem itm in t.KitchenItems())
                    {
                        items += "\t - " + itm.GetName() + "\n";
                    }

                    this.view.ShowTicket(((TakeAway)t.Parent()).Name(), items, (DateTime.Now - t.TimeOpened()).ToString(), i);
                }
                i++;
            }
            this.view.ShowMessage("Enter a ticket number to mark it as complete.\nEnter 'm' to go back to the menu.");
            while (true)
            {

                string input = Console.ReadLine();
                int selection;
                if (!Int32.TryParse(input, out (selection)))
                {
                    if (input != "m")
                    {
                        this.view.ShowError("Enter a valid option");
                        continue;
                    }
                    else
                    {
                        // m is entered - go back to the menu.
                        MenuHolder.GetMenuController().Show();
                    }
                }
                else
                {
                    if (selection >= ticketMan.Tickets().Count || selection < 0)
                    {
                        this.view.ShowError("Enter a valid option");
                        continue;
                    }
                    else
                    {
                        // Business logic to mark ticket as complete, and fold items into order.
                        // markoff[input] - or something to that effect
                    }
                }
            }
        }
    }
}
