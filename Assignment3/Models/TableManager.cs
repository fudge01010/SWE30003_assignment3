using Assignment3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3.Models
{
    class TableManager
    {
        private List<Table> tables;
        public TableManager()
        {
            // constructor
            tables = DBManager.LoadFromDB_Tables();
        }

        public void AddTable(Table toAdd)
        {
            tables.Add(toAdd);
        }

        public Table GetTable(int i)
        {
            return tables.Find(x => x.GetID() == i);
        }
    }
}
