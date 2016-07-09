using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using CriminalIntent.Core;
using CriminalIntent.Core.Models;
using SQLite.Net.Platform.XamarinAndroid;

namespace CriminalIntent.DAO
{
    public class CrimeLab
    {
        private static CrimeLab _sCrimeLab;
        private Context _context;
        private readonly CrimesRepository _repository;

        public List<Crime> Crimes { get { return _repository.GetAllCrimes(); } }

        private CrimeLab(Context context)
        {
            _context = context;
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "crimeDb.db3");
            _repository = new CrimesRepository(new SQLitePlatformAndroid(), dbPath);
        }

        public static CrimeLab Get(Context context)
        {
            return _sCrimeLab ?? (_sCrimeLab = new CrimeLab(context));
        }

        public Crime GetCrime(Guid id)
        {
            return _repository.GetCrime(id);
        }

        public void AddCrime(Crime crime)
        {
            _repository.AddCrime(crime);
        }

        public void UpdateCrime(Crime crime)
        {
            _repository.UpdateCrime(crime);
        }

        public void RemoveCrime(Crime crime)
        {
            _repository.RemoveCrime(crime);
        }
    }
}

