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
            //SQLiteCommand sqlite_cmd2;
            //sqlite_cmd2 = sqlite_conn.CreateCommand();
            //// add test customer to 
            //sqlite_cmd2.CommandText = "INSERT INTO Customers (CustomerID, CustomerName, CustomerPhone, CustomerAddress, CustomerDateCreated) VALUES ($id, $name, $phone, $address, $date)";
            //sqlite_cmd2.Parameters.AddWithValue("$id", 4);
            //sqlite_cmd2.Parameters.AddWithValue("$name", "pete tester");
            //sqlite_cmd2.Parameters.AddWithValue("$phone", "89858985");
            //sqlite_cmd2.Parameters.AddWithValue("$address", "4 martin crescent, Caulfied");
            //sqlite_cmd2.Parameters.AddWithValue("$date", DateTime.Now);
            //sqlite_cmd2.ExecuteNonQuery();

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
            sqlite_cmd.Parameters.AddWithValue("$created", DateTime.Now.ToString());

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

    }
}
