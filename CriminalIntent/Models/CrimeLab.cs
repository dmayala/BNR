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
        public List<Crime> Crimes { get ; private set; }

        private CrimeLab(Context context)
        {
            Crimes = new List<Crime>();
        }

        public static CrimeLab Get(Context context)
        {
            return _sCrimeLab ?? (_sCrimeLab = new CrimeLab(context));
        }

        public Crime GetCrime(Guid id)
        {
            return Crimes.First(c => c.Id.Equals(id));
        }

        public void AddCrime(Crime crime)
        {
            Crimes.Add(crime);
        }

        public void RemoveCrime(Crime crime)
        {
            Crimes.Remove(crime);
        }
    }
}

