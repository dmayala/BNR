using Homepwner.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homepwner.Stores
{
    public class ItemStore
    {
        public List<Item> AllItems { get; } = new List<Item>();

        public ItemStore()
        {
            for (var i = 0; i < 5; i++)
            {
                CreateItem();
            }
        }

        public Item CreateItem()
        {
            var newItem = Item.NewRandomItem();
            AllItems.Add(newItem);
            return newItem;
        }
    }
}
