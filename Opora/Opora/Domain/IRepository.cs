using System.Collections.Generic;

namespace Opora.Domain
{
    public interface IRepository<T, TKey>
    {
        void AddItem(T item);

        void UpdateItem(T item);

        void DeleteItem(TKey id);

        T GetItem(TKey id);

        IEnumerable<T> GetItems();
    }
}