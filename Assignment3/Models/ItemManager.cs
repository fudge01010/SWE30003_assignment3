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

        public string GetFormattedItems()
        {
            String s = "";
            foreach (IItem i in GetItems())
            {
                s += ("| " + i.GetId().ToString().PadRight(3) + " | ");
                s += (i.GetName().PadRight(20) + " | ");
                s += (i.GetDescription().PadRight(50) + " | ");
                s += (i.GetPrice().ToString().PadRight(5) + " | \n");
            }
            return s;
        }
    }
}
