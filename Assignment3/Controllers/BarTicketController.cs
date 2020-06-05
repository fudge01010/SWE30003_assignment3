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
            DineIn testOrder = new DineIn(3);
            FoodItem testFood = new FoodItem(1, "cheeseburger", "a cheeseburger", 12.92f);
            DrinkItem testDrink = new DrinkItem(2, "coffee", "a coffee", 4.25f);
            Ticket testTicket = new Ticket(testOrder, new List<IItem>(){ testFood, testDrink });
            ticketMan.AddTicket(testTicket);
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
            foreach (Ticket t in ticketMan.Tickets())
            {
                if (t.Parent().GetType() == typeof(DineIn))
                {
                    // it's a dine in order
                    this.view.ShowTicket(t.TableNumber(), "\t-Cheeseburger\n\t-Pizza", "00:22:02", i);
                }
                if (t.Parent().GetType() == typeof(TakeAway) )
                {
                    // it's a takeaway ticket
                    this.view.ShowTicket(((TakeAway)t.Parent()).Name(), "\t-Cheeseburger\n\t-Pizza", "00:22:02", i);
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
                    if (selection >= ticketMan.Tickets().Count || selection < 0)
                    {
                        this.view.ShowError("Enter a valid option");
                        continue;
                    } else
                    {
                        // Business logic to mark ticket as complete, and fold items into order.
                        // markoff[input] - or something to that effect
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
