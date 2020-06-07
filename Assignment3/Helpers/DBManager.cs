using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text;
using Assignment3.Models;
using System.Linq.Expressions;
using System.IO;

namespace Assignment3.Helpers
{
    static public class DBManager
    {
        private static SQLiteConnection sqlite_conn;

        public static void CreateConnection()
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;Compress=True;");
            try
            {
                sqlite_conn.Open();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void CloseConnection()
        {
            sqlite_conn.Close();
        }

        static public void CreateTable()
        {
            
        }

        public static List<Table> LoadFromDB_Tables()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Tables";

            List<Table> tables = new List<Table>();

            SQLiteDataReader sqlite_reader = sqlite_cmd.ExecuteReader();
            while (sqlite_reader.Read())
            {
                tables.Add(new Table(sqlite_reader.GetInt32(0), sqlite_reader.GetString(1)));
            }

            return tables;
        }

        public static Table GetTable(int tableId)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Tables WHERE TableID = $id";
            sqlite_cmd.Parameters.AddWithValue("$id", tableId);

            SQLiteDataReader sqlite_reader = sqlite_cmd.ExecuteReader();
            sqlite_reader.Read();
            return new Table(sqlite_reader.GetInt32(0), sqlite_reader.GetString(1));
        }

        public static List<Reservation> LoadFromDB_Reservations()
        {
            List<Reservation> res = new List<Reservation>();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Reservations";
            
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                int id, custId, tableId = 0;
                DateTime resTime;
                id = reader.GetInt32(0);
                custId = reader.GetInt32(1);
                if (reader[2].GetType() != typeof(DBNull))
                {
                    tableId = reader.GetInt32(2);
                }
                resTime = Convert.ToDateTime(reader.GetString(3));
                res.Add(new Reservation(id, custId, tableId, resTime));
            }
            return res;
        }

        public static List<Customer> LoadFromDB_Customers()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Customers";

            List<Customer> customers = new List<Customer>();
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = 0;
                string name, phone, address = null;
                DateTime created;
                id = reader.GetInt32(0);
                if (reader[1].GetType() != typeof(DBNull))
                    name = reader.GetString(1);
                else
                    name = null;
                if (reader[2].GetType() != typeof(DBNull))
                    phone = reader.GetString(2);
                else
                    phone = null;
                if (reader[3].GetType() != typeof(DBNull))
                { 
                Console.WriteLine(reader[3].GetType().ToString());
                address = reader.GetString(3);
                 }
                else
                    address = null;
                created = Convert.ToDateTime(reader.GetString(4));
                customers.Add(new Customer(id, name, phone, address, created));
            }
            return customers;
        }

        public static Customer GetCustomer(int custId)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Customers WHERE CustomerID = $cid";
            sqlite_cmd.Parameters.AddWithValue("$cid", custId);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            reader.Read();
            string name, phone, address = null;
            DateTime created;
            if (reader[1].GetType() != typeof(DBNull))
                name = reader.GetString(1);
            else
                name = null;
            if (reader[2].GetType() != typeof(DBNull))
                phone = reader.GetString(2);
            else
                phone = null;
            if (reader[3].GetType() != typeof(DBNull))
            {
                Console.WriteLine(reader[3].GetType().ToString());
                address = reader.GetString(3);
            }
            else
                address = null;
            created = Convert.ToDateTime(reader.GetString(4));
            return new Customer(custId, name, phone, address, created);
        }

        public static List<IItem> LoadFromDB_Items()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Products";

            List<IItem> items = new List<IItem>();
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetInt16(4) == 0)
                {
                    //it's a food item.
                    int id = reader.GetInt32(0);
                    string prodName = reader.GetString(1);
                    string desc = "";
                    if (reader[2].GetType() != typeof(DBNull))
                        desc = reader.GetString(2);
                    else
                        desc = null;
                    //Console.WriteLine(reader[3].GetType().ToString());
                    string price = reader.GetDecimal(3).ToString();

                    items.Add(new FoodItem(id, prodName, desc, float.Parse(price)));
                } else if (reader.GetInt16(4) == 1)
                {
                    // it's a drink item
                    int id = reader.GetInt32(0);
                    string prodName = reader.GetString(1);
                    string desc = "";
                    if (reader[2].GetType() != typeof(DBNull))
                        desc = reader.GetString(1);
                    else
                        desc = null;
                    //Console.WriteLine(reader[3].GetType().ToString());
                    string price = reader.GetDecimal(3).ToString();
                    items.Add(new DrinkItem(id, prodName, desc, float.Parse(price)));
                } else
                {
                    // we should never be here - it means the database has a product type that isn't 1 or 0.
                    throw new InvalidDataException();
                }
            }
            return items;
        }

        public static int GetNextProductId()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT seq FROM sqlite_sequence WHERE name = 'Products'";
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0) + 1;
        }

        public static int GetNextCustomerId()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT seq FROM sqlite_sequence WHERE name = 'Customers'";
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0) + 1;
        }

        public static int GetNextSaleId()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT seq FROM sqlite_sequence WHERE name = 'Sales'";
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0) + 1;
        }

        public static int GetNextResId()
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT seq FROM sqlite_sequence WHERE name = 'Reservations'";
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0) + 1;
        }

        public static void AddProduct(IItem productToAdd)
        {
            // extract info from product
            int itemType;
            if (productToAdd.GetType() == typeof(FoodItem))
            {
                itemType = 0;
            } else if (productToAdd.GetType() == typeof(DrinkItem))
            {
                itemType = 1;
            } else
            {
                throw new Exception();
            }

            // create db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add test customer to 
            sqlite_cmd.CommandText = "INSERT INTO Products (ProductID, ProductName, ProductDesc, ProductPrice, ProductType) VALUES ($id, $name, $desc, $price, $type)";
            sqlite_cmd.Parameters.AddWithValue("$id", productToAdd.GetId());
            sqlite_cmd.Parameters.AddWithValue("$name", productToAdd.GetName());
            sqlite_cmd.Parameters.AddWithValue("desc", productToAdd.GetDescription());
            sqlite_cmd.Parameters.AddWithValue("price", Math.Round(productToAdd.GetPrice(),2));
            sqlite_cmd.Parameters.AddWithValue("type", itemType);

            // run that shit
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void AddReservation(Reservation resToAdd)
        {
            // create db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add test customer to 
            sqlite_cmd.CommandText = "INSERT INTO Reservations (ReservationID, CustomerID, TableID, ReservationDateTime) VALUES ($resid, $custid, $tableid, $resdatetime)";
            sqlite_cmd.Parameters.AddWithValue("$resid", resToAdd.GetId());
            sqlite_cmd.Parameters.AddWithValue("$custId", resToAdd.GetCustId());
            sqlite_cmd.Parameters.AddWithValue("$tableid", resToAdd.GetTableId());
            sqlite_cmd.Parameters.AddWithValue("$resdatetime", resToAdd.GetReservationTime().ToString("yyyy-MM-dd HH:mm:ss"));

            // insert it
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void UpdateProduct(IItem productToUpdate)
        {
            // create db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add test customer to 
            sqlite_cmd.CommandText = "UPDATE Products SET ProductName = $name, ProductDesc = $desc, ProductPrice = $price WHERE ProductID = $id";
            sqlite_cmd.Parameters.AddWithValue("$id", productToUpdate.GetId());
            sqlite_cmd.Parameters.AddWithValue("$name", productToUpdate.GetName());
            sqlite_cmd.Parameters.AddWithValue("desc", productToUpdate.GetDescription());
            sqlite_cmd.Parameters.AddWithValue("price", Math.Round(productToUpdate.GetPrice(), 2));

            // run that shit
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void AddCustomer(Customer customer)
        {
            // create db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add test customer to 
            sqlite_cmd.CommandText = "INSERT INTO Customers (CustomerID, CustomerName, CustomerPhone, CustomerAddress, CustomerDateCreated) VALUES ($id, $name, $phone, $address, $created)";
            sqlite_cmd.Parameters.AddWithValue("$id", customer.GetId());
            sqlite_cmd.Parameters.AddWithValue("$name", customer.GetName());
            sqlite_cmd.Parameters.AddWithValue("$phone", customer.GetPhone());
            sqlite_cmd.Parameters.AddWithValue("$address", customer.GetAddress());
            sqlite_cmd.Parameters.AddWithValue("$created", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            // run that shit
            sqlite_cmd.ExecuteNonQuery();
        }

        public static int LookupCustomer(string phNumb)
        {
            // query to see if the number exists. returns -1 if not; otherwise returns customerId
            // create db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add test customer to 
            sqlite_cmd.CommandText = "SELECT CustomerID FROM Customers WHERE CustomerPhone = $phone";
            sqlite_cmd.Parameters.AddWithValue("$phone", phNumb);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            //reader.Read();
            if (!reader.Read())
            {
                return -1;
            }
            else
            {
                return reader.GetInt32(0);
            }
        }

        public static string LookupCustomerName(int id)
        {
            // query to see if the number exists. returns -1 if not; otherwise returns customerId
            // create db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add test customer to 
            sqlite_cmd.CommandText = "SELECT CustomerName FROM Customers WHERE CustomerID = $id";
            sqlite_cmd.Parameters.AddWithValue("$id", id);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            reader.Read();
            string custName = "";
            if (reader[0].GetType() != typeof(DBNull))
                custName = reader.GetString(0);
            return custName;
        }

        public static void SaveOrder(Order orderToSave)
        {
            // save the supplied order into the db.
            // first, we save the total cost of the order + relevant details (to generate the required SaleID, which is a FK/PK in the SaleItems table
            // get the next sale ID we're going to use for this row:
            int saleID = GetNextSaleId();

            // create Sale db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add sql with parameters 
            sqlite_cmd.CommandText = "INSERT INTO Sales (SaleID, SaleDate, Saletotal, CustomerID) VALUES ($id, $date, $total, $custid)";
            sqlite_cmd.Parameters.AddWithValue("$id", saleID);
            sqlite_cmd.Parameters.AddWithValue("$total", Math.Round(orderToSave.GetOrderCost(),2));
            sqlite_cmd.Parameters.AddWithValue("$custid", orderToSave.CustomerID());
            sqlite_cmd.Parameters.AddWithValue("$date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            // run it
            sqlite_cmd.ExecuteNonQuery();

            // great - now we have a sale in the Sale table. We can use that ID to insert into SaleProducts table:
            // loop over the items in the sale
            foreach (IItem itm in orderToSave.ItemsOnOrder())
            {
                // refresh the DB query
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO SaleItems (SaleID, ProductID) VALUES ($id, $pid)";
                sqlite_cmd.Parameters.AddWithValue("$id", saleID);
                sqlite_cmd.Parameters.AddWithValue("$pid", itm.GetId());
                // run that shit
                sqlite_cmd.ExecuteNonQuery();
            }

            // these aren't the droids you're looking for
        }

        public static List<string> GetSales()
        {
            List<string> theSales = new List<string>();
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add sql with parameters 
            sqlite_cmd.CommandText = "SELECT SaleID, SaleDate, SaleTotal, CustomerName FROM Sales INNER JOIN Customers ON Customers.CustomerID = Sales.CustomerID";

            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            while (reader.Read())
            { 
                //it's a food item.
                int saleid = reader.GetInt32(0);
                string saleDate = reader.GetString(1);
                string price = reader.GetDecimal(2).ToString();
                string custName = "";
                if (reader[3].GetType() != typeof(DBNull))
                    custName = reader.GetString(3);

                theSales.Add("| " + saleid.ToString().PadRight(3) + "| " + saleDate.PadRight(28) + "| " + price.PadRight(6) + "| " + custName.PadRight(20) + "|");
            }

            return theSales;
        }

        public static Reservation GetReservation(int resId)
        {
            // create db query
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            // add test customer to 
            sqlite_cmd.CommandText = "SELECT * FROM Reservations WHERE ReservationID = $id";
            sqlite_cmd.Parameters.AddWithValue("$id", resId);
            SQLiteDataReader reader = sqlite_cmd.ExecuteReader();
            //reader.Read();
            if (!reader.Read())
            {
                return null;
            }
            else
            {
                return new Reservation(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), DateTime.Parse(reader.GetString(3)));
            }
        }

    }
}
