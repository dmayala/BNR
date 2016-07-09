using System;
using SQLite.Net.Attributes;

namespace CriminalIntent.Core.Models
{
    public class Crime
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Solved { get; set; }
        public string Suspect { get; set; }
        public string PhoneNumber { get; set; }

        public Crime()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}

