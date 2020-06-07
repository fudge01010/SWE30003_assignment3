using Assignment3.Helpers;
using Assignment3.Models;
using Assignment3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Assignment3.Controllers
{
    class OrderController : IController
    {
        private OrderView view;
        private List<Order> orders;
        private TableManager tableManager;
        private ItemManager itemManager;
        private TicketManager ticketManager;

        public OrderController(TableManager tableman, ItemManager itemMan, TicketManager ticketMan)
        {
            CreateView();
            orders = new List<Order>();
            tableManager = tableman;
            itemManager = itemMan;
            ticketManager = ticketMan;
        }
        public string GetPrettyName()
        {
            return "Orders Page";
        }

        public void CreateView()
        {
            this.view = new OrderView();
        }

        public void SetView(IView view)
        {
            this.view = (OrderView)view;
        }

        public void Show()
        {
            view.Clear();
            if (orders.Count == 0)
            {
                view.ShowMessage("No currently active orders.\n\n");
            } else
            {
                view.ShowView();
                for (int o = 0; o < orders.Count; o++)
                {

                    view.ShowOrder(o, orders[o].TableNumber(), orders[o].OpenedFor(), orders[o].FormattedItemsOnOrder(), orders[o].GetOrderCost());
                }
                
            }
            while (true)
            {
                // main command input loop
                view.ShowMessage("Enter 'n' for a new order, or 'e' to perform actions on an existing order. Enter 'm' to go back to the main menu.");
                string input = Console.ReadLine();
                if (input == "e" && orders.Count <= 0)
                {
                    view.ShowError("There are no active orders to edit.");
                }
                else if (input == "e")
                {
                    // EDIT ORDER
                    int numb = -1;
                    while (true)
                    {
                        view.ShowMessage("enter the number of the order to edit");
                        input = Console.ReadLine();
                        int.TryParse(input, out (numb));
                        if (numb >= orders.Count || numb < 0)
                        {
                            view.ShowError("Please enter a valid number in range");
                        } else
                        {
                            break;
                        }
                    }
                    EditOrder(orders[numb]);
                }
                else if (input == "n")
                {
                    // NEW ORDER
                    view.ShowMessage("Input 'd' for dine-in, 't' for takeaway");
                    input = Console.ReadLine();
                    while (true)
                    {
                        if (input == "d")
                        {
                            view.Clear();
                            view.ShowMessage("Please enter the customers phone number:");
                            string ph = Console.ReadLine();
                            while (true)
                            {
                                if (ph.Length < 8 || ph.Length > 10)
                                {
                                    view.ShowError("Please enter a valid phone number");
                                    ph = Console.ReadLine();
                                } else
                                {
                                    break;
                                }
                            }
                            // we have a phone number. look up in DB, create user if doesn't exist.
                            int custId = DBManager.LookupCustomer(ph);
                            if (custId == -1)
                            {
                                view.ShowMessage("New customer. Enter a name (optional)");
                                string name = "";
                                name = Console.ReadLine();
                                view.ShowMessage("Enter an address (optional)");
                                string address = "";
                                address = Console.ReadLine();

                                // get the next avail custID
                                int newId = DBManager.GetNextCustomerId();

                                // create the customer, and write to DB
                                Customer cust = new Customer(newId, name, ph, address, DateTime.Now);
                                DBManager.AddCustomer(cust);

                                // find the table
                                view.ShowMessage("Enter the table number customer is seated at:");
                                int tableNumber = int.Parse(Console.ReadLine());

                                // look that table up from the tableManager
                                Table orderTable = tableManager.GetTable(tableNumber);
                                // now lets make the order
                                NewDineInOrder(cust, orderTable);
                            } else
                            {
                                view.ShowMessage("existing customer found.");
                                // find the table
                                view.ShowMessage("Enter the table number customer is seated at:");
                                int tableNumber = int.Parse(Console.ReadLine());

                                // look that table up from the tableManager
                                Table orderTable = tableManager.GetTable(tableNumber);
                                // now lets make the order
                                NewDineInOrder(DBManager.GetCustomer(custId), orderTable);

                            }
                        }
                        else if (input == "t")
                        {
                            // take-away
                            view.Clear();
                            view.ShowMessage("Please enter the customers phone number:");
                            string ph = Console.ReadLine();
                            while (true)
                            {
                                if (ph.Length < 8 || ph.Length > 10)
                                {
                                    view.ShowError("Please enter a valid phone number");
                                    ph = Console.ReadLine();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            // we have a phone number. look up in DB, create user if doesn't exist.
                            int custId = DBManager.LookupCustomer(ph);
                            if (custId == -1)
                            {
                                view.ShowMessage("New customer. Enter a name (optional)");
                                string name = "";
                                name = Console.ReadLine();
                                view.ShowMessage("Enter an address (optional)");
                                string address = "";
                                address = Console.ReadLine();

                                // get the next avail custID
                                int newId = DBManager.GetNextCustomerId();

                                // create the customer, and write to DB
                                Customer cust = new Customer(newId, name, ph, address, DateTime.Now);
                                DBManager.AddCustomer(cust);

                                NewTakeAwayOrder(cust);
                            }
                            else
                            {
                                // now lets make the order
                                NewTakeAwayOrder(DBManager.GetCustomer(custId));

                            }
                        }
                        else
                        {
                            view.ShowError("Please enter d or t");
                        }
                    }
                }
                else if (input == "m")
                {
                    MenuHolder.GetMenuController().Show();
                } else
                {
                    throw new Exception();
                }
            }
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public void NewDineInOrder(Customer cust, Table table)
        {
            //
            view.Clear();
            view.ShowMessage("New dine-in order for customer ID: " + cust.GetId() + ". " + table.GetName());
            view.ShowMessage("");
            orders.Add(new DineIn(cust.GetId(), table.GetID()));
            view.ShowMessage("Order created. press enter to go back to the orders page.");
            Console.ReadLine();
            Show();
        }

        public void NewTakeAwayOrder(Customer cust)
        {
            view.Clear();
            view.ShowMessage("New take-away order for customer ID: " + cust.GetId() + ". ");
            orders.Add(new TakeAway(cust.GetId(), cust.GetName()));
            view.ShowMessage("Order created. press enter to go back to the orders page.");
            Console.ReadLine();
            Show();
        }

        public void EditOrder(Order orderToEdit)
        {
            //
            view.Clear();
            view.ShowMessage("Enter 'a' to add items to the order. 'p' to mark the order as paid. 'r' to remove items from the order.");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "a":
                        // add items to order
                        orderToEdit.SetTempTicket(new Ticket(orderToEdit));
                        AddItems(orderToEdit);
                        break;
                    case "p":
                        // mark order as paid
                        break;
                    case "r":
                        // remove items from the order
                        break;
                    default:
                        view.ShowError("Please enter either 'a', 'p' or 'r'");
                        break;
                }
            }
        }

        public void AddItems(Order orderEditing)
        {
            while (true)
            {
                view.Clear();
                view.ShowOrder(orderEditing.TableNumber(), orderEditing.OpenedFor(), orderEditing.FormattedTempItemsOnOrder(), orderEditing.GetTempOrderCost());
                view.ShowMessage("==========================================");
                view.ShowMessage("Items available to add:");
                view.ShowMessage(itemManager.GetFormattedItems());
                view.ShowMessage("Enter the product ID to add to this order, or press 's' to save, or 'c' to cancel changes.");
            
                string cmd = Console.ReadLine();
                int i = -1;
                bool status = Int32.TryParse(cmd, out (i));
                if (cmd == "s")
                {
                    // save the order, generate ticket
                    // first save the temp ticket as a real ticket
                    ticketManager.AddTicket(orderEditing.TempTicket());

                    // add the tempTicket items to the order for real
                    orderEditing.AddItems(orderEditing.TempTicket().Items());

                    // delete the temp ticket reference
                    orderEditing.SetTempTicket(null);
                    Show();
                }
                else if (cmd == "c")
                {
                    // purge the temp ticket
                    orderEditing.SetTempTicket(null);
                    Show();
                }
                else if (!status)
                {
                    view.ShowError("Please enter a valid letter command.");
                }
                else if (!(i > 0 && i < itemManager.GetItems().Count + 1))
                {
                    view.ShowError("Please enter an item ID in range.");
                }
                else
                {
                    // it's a number. lookup item and add to ticket.
                    orderEditing.TempTicket().AddItem(itemManager.GetItem(i));

                    // go back to the start of this screen
                    AddItems(orderEditing);
                }

            }

        }
        public string ItemsOnOrder(Order order)
        {
            string s = "";
            foreach (IItem i in order.ItemsOnOrder())
            {
                s += ("| " + i.GetId().ToString().PadRight(3) + " | ");
                s += (i.GetName().PadRight(20) + " | ");
                s += (i.GetDescription().PadRight(50) + " | ");
                s += (i.GetPrice().ToString().PadRight(5) + " |\n");
            }
            return s;
        }
    }
}
