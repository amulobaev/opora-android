using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Opora.Domain
{
    public class OporaDatabase : IDatabase
    {
        private readonly SQLiteConnection database;

        public OporaDatabase()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(path, "opora.db3");
            database = new SQLiteConnection(dbPath);
            database.CreateTable<MeasurementEntity>();
            database.CreateTable<PillarEntity>();
        }

        public void AddItem<T>(T item)
        {
            
        }

        public void DeleteItem<T>(Guid id)
        {
            
        }

        public T GetItem<T>(Guid id) where T : new()
        {
            return database.Get<T>(id);
        }

        public IEnumerable<T> GetItems<T>() where T : new()
        {
            return database.Table<T>().ToList();
        }

        public void UpdateItem<T>(T item)
        {
            
        }
    }
}