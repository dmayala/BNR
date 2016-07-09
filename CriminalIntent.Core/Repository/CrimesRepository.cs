using System;
using System.Collections.Generic;
using System.Linq;
using CriminalIntent.Core.Models;
using SQLite.Net;
using SQLite.Net.Interop;

namespace CriminalIntent.Core
{
    public class CrimesRepository
    {
        private readonly string _path;
        private ISQLitePlatform _platform;

        public CrimesRepository(ISQLitePlatform platform, string path)
        {
            _platform = platform;
            _path = path;
            CreateTable();
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_platform, _path);
        }

        public int AddCrime(Crime newCrime)
        {
            var db = GetConnection();
            using (db)
            {
                return db.Insert(newCrime);
            }
        }

        public int RemoveCrime(Crime crime)
        {
            var db = GetConnection();
            using (db)
            {
                return db.Delete(crime);
            }
        }

        public int UpdateCrime(Crime crime)
        {
            var db = GetConnection();
            using (db)
            {
                return db.Update(crime);
            }
        }

        public Crime GetCrime(Guid id)
        {
            var db = GetConnection();
            using (db)
            {
                return db.Get<Crime>(id);
            }
        }

        public List<Crime> GetAllCrimes()
        {
            var db = GetConnection();
            using (db)
            {
                return (from i in db.Table<Crime>() select i).ToList();
            }
        }

        private int CreateTable()
        {
            var db = GetConnection();
            using (db)
            {
                return db.CreateTable<Crime>();
            }
        }
    }
}

