using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment3.Models
{
    public class Table
    {
        private readonly int tableID;
        private readonly string tableName;

        public Table(int id, string name)
        {
            tableID = id;
            tableName = name;
        }

        public string GetName()
        {
            return tableName;
        }

        public int GetID()
        {
            return tableID;
        }
    }
}
