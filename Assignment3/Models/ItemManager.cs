using Assignment3.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Assignment3.Models
{
    class ItemManager
    {
        private readonly List<IItem> items;

        public ItemManager ()
        {
            // constructor

            items = DBManager.LoadFromDB_Items();
        }

        public void AddItem(IItem item)
        {
            items.Add(item);
        }

        public IItem GetItem(int itemId)
        {
            return items.Find(x => x.GetId() == itemId);
        }

        public List<IItem> GetItems()
        {
            return items;
        }
    }
}
