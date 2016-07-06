using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using CriminalIntent.Models;

namespace CriminalIntent
{
    public class CrimeLab
    {
        private static CrimeLab _sCrimeLab;
        public List<Crime> Crimes { get; private set; } = new List<Crime>();

        private CrimeLab(Context context)
        {
            for (int i = 0; i < 100; i++)
            {
                Crimes.Add(new Crime() { 
                    Title = $"Crime {i}",
                    Solved = (i % 2 == 0)
                });
            }
        }

        public static CrimeLab Get(Context context)
        {
            return _sCrimeLab ?? (_sCrimeLab = new CrimeLab(context));
        }

        public Crime GetCrime(Guid id)
        {
            return Crimes.First(c => c.Id.Equals(id));
        }
    }
}

