using Foundation;
using System;
using Homepwner.Extensions;

namespace Homepwner.Models
{
    public class Item : NSCoding
    {
        private static readonly Random rand = new Random();

        public string Name { get; set; }
        public int ValueInDollars { get; set; }
        public string SerialNumber { get; set; }
        public string ImageKey { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now;

        public Item(string name, int valueInDollars, string serialNumber)
        {
            Name = name;
            ValueInDollars = valueInDollars;
            SerialNumber = serialNumber;
            ImageKey = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
        }

        [Export("initWithCoder:")]
        public Item(NSCoder decoder)
        {
            Name = decoder.DecodeObject("Name").ToString();
            ValueInDollars = decoder.DecodeInt("ValueInDollars");
            SerialNumber = decoder.DecodeObject("SerialNumber").ToString();
            ImageKey = decoder.DecodeObject("ImageKey").ToString();

            var date = decoder.DecodeObject("DateCreated") as NSDate;
            DateCreated = date.ToDateTime();
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
            var randomSerial = Guid.NewGuid().ToString().Split('-')[0];

            return new Item(randomName, randomValue, randomSerial);
        }

        public override void EncodeTo(NSCoder encoder)
        {
            encoder.Encode(new NSString(Name), "Name");
            encoder.Encode(ValueInDollars, "ValueInDollars");
            encoder.Encode(new NSString(SerialNumber), "SerialNumber");
            encoder.Encode(new NSString(ImageKey), "ImageKey");
            encoder.Encode(DateCreated.ToNSDate(), "DateCreated");
        }
    }
}
