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
        }

        public void AddItem(IItem item)
        {
            items.Add(item);
        }
    }
}
