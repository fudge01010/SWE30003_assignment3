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
                    address = reader.GetString(3);
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
                        desc = reader.GetString(1);
                    else
                        desc = null;
                    string price = reader.GetString(3);
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
                    string price = reader.GetString(3);
                    items.Add(new DrinkItem(id, prodName, desc, float.Parse(price)));
                } else
                {
                    // we should never be here - it means the database has a product type that isn't 1 or 0.
                    throw new InvalidDataException();
                }
            }
            return items;
        }

    }
}
