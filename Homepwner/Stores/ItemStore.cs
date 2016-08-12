using Foundation;
using Homepwner.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Homepwner.Stores
{
    public class ItemStore
    {
        public List<Item> AllItems { get; } = new List<Item>();
        public NSUrl ItemArchiveUrl
        {
            get
            {
                var documentDirectories =
                    NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User);
                var documentDirectory = documentDirectories[0];
                return documentDirectory.Append("items.archive", true);
            }
        }

        public ItemStore()
        {
            var archivedItems = NSKeyedUnarchiver.UnarchiveFile(ItemArchiveUrl.Path) as NSArray;

            for (nuint i = 0; i < archivedItems.Count; i++)
            {
                AllItems.Add(archivedItems.GetItem<Item>(i));
            }
        }

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

        public bool SaveChanges()
        {
            Debug.WriteLine($"Saving items to {ItemArchiveUrl.Path}");
            NSMutableArray newArray = new NSMutableArray(); 

            foreach (Item item in AllItems) {
                newArray.Add(item);
            }

            return NSKeyedArchiver.ArchiveRootObjectToFile(newArray, ItemArchiveUrl.Path);
        }
    }
}
