using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;

namespace Opora.Domain
{
    public class OporaDatabase : IDatabase
    {
        private readonly SQLiteConnection _database;

        public OporaDatabase()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(path, "opora.db3");

            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<MeasurementEntity>();
            _database.CreateTable<PillarEntity>();
        }

        public void AddItem<T>(T item)
        {
            _database.Insert(item);
        }

        public void DeleteItem<T>(Guid id)
        {
        }

        public T GetItem<T>(Guid id) where T : BaseEntity, new()
        {
            return _database.Table<T>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> GetItems<T>() where T : new()
        {
            return _database.Table<T>().ToList();
        }

        public void UpdateItem<T>(T item)
        {
            _database.Update(item);
        }
    }
}