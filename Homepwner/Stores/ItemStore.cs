using Homepwner.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homepwner.Stores
{
    public class ItemStore
    {
        public List<Item> AllItems { get; } = new List<Item>();

        public void MoveItemAtIndex(int fromIndex, int toIndex)
        {
            if (fromIndex == toIndex) return;

            // Get reference to object being moved so you can reinsert it
            var movedItem = AllItems[fromIndex];

            // Remove item from array
            AllItems.RemoveAt(fromIndex);

            // Insert item in array at new location
            AllItems.Insert(toIndex, movedItem);
        }

        public Item CreateItem()
        {
            var newItem = Item.NewRandomItem();
            AllItems.Add(newItem);
            return newItem;
        }

        public void RemoveItem(Item item)
        {
            AllItems.Remove(item);
        }
    }
}
