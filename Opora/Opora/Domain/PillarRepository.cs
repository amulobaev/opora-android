using System;
using System.Collections.Generic;

using Opora.Models;

namespace Opora.Domain
{
    public class PillarRepository : IRepository<Pillar, Guid>
    {
        private readonly List<Pillar> _items;

        public PillarRepository()
        {
            _items = new List<Pillar>()
            {
                new Pillar {Name = "СС 156.6", Height = 15.6, Taper = 11.7 },
                new Pillar {Name = "СС 136.6", Height = 13.6, Taper = 10.1 }
            };
        }

        public void AddItem(Pillar model)
        {
            _items.Add(model);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Pillar GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pillar> GetItems()
        {
            return _items;
        }

        public void UpdateItem(Pillar model)
        {
            throw new NotImplementedException();
        }
    }
}