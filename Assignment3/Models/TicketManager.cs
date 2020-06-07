using Assignment3.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    class TicketManager
    {
        private List<Ticket> tickets;

        public TicketManager()
        {
            // constructor
            tickets = new List<Ticket>();
        }

        public void AddTicket(Ticket tic)
        {
            tickets.Add(tic);
        }

        public void removeTicket(Ticket tic)
        {
            tickets.Remove(tic);
        }

        public List<Ticket> FindTicketByTableNumber(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> Tickets()
        {
            return tickets;
        }

        public List<Ticket> BarTickets()
        {
            //
            List<Ticket> barTickets = new List<Ticket>();
            foreach (Ticket t in Tickets())
            {
                if (t.HasDrinkItems())
                {
                    barTickets.Add(t);
                }
            }
            return barTickets;
        }

        public List<Ticket> KitchenTickets()
        {
            List<Ticket> foodTickets = new List<Ticket>();
            foreach (Ticket t in Tickets())
            {
                if (t.HasFoodItems())
                {
                    foodTickets.Add(t);
                }
            }
            return foodTickets;
        }
    }
}
