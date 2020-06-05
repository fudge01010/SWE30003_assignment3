using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Helpers
{
    static public class DBManager
    {
        private static SQLiteConnection sqlite_conn;


        public static void CreateConnection()
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
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

    }
}
