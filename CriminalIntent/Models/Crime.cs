using System;
namespace CriminalIntent.Models
{
    public class Crime
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Solved { get; set; }

        public Crime()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}

