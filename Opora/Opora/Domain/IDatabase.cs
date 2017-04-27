using System;
using System.Collections.Generic;

namespace Opora.Domain
{
    public interface IDatabase
    {
        T GetItem<T>(Guid id) where T : new();

        IEnumerable<T> GetItems<T>() where T : new();

        void AddItem<T>(T item);

        void UpdateItem<T>(T item);

        void DeleteItem<T>(Guid id);
    }
}