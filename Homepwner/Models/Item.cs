using System;
using System.Collections.Generic;
using System.Text;

namespace Homepwner.Models
{
    public class Item
    {
        private static readonly Random rand = new Random();

        public string Name { get; set; }
        public int ValueInDollars { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now;

        public Item(string name, int valueInDollars, string serialNumber)
        {
            Name = name;
            ValueInDollars = valueInDollars;
            SerialNumber = serialNumber;
            DateCreated = DateTime.Now;
        }

        public static Item NewRandomItem()
        {
            var adjectives = new string[] { "Fluffy", "Rusty", "Shiny" };
            var nouns = new string[] { "Bear", "Spork", "Mac" };

            var idx = rand.Next(adjectives.Length);
            var randomAdjective = adjectives[idx];

            idx = rand.Next(nouns.Length);
            var randomNoun = nouns[idx];

            var randomName = $"{randomAdjective} {randomNoun}";
            var randomValue = rand.Next(100);
            var randomSerial = new Guid().ToString().Split('-')[0];

            return new Item(randomName, randomValue, randomSerial);
        }
    }
}
