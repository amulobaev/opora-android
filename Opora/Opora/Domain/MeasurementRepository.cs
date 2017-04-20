using System;
using System.Collections.Generic;

using Opora.Models;

namespace Opora.Domain
{
    public class MeasurementRepository : IRepository<Measurement, Guid>
    {
        public void AddItem(Measurement model)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Measurement GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Measurement> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Measurement model)
        {
            throw new NotImplementedException();
        }
    }
}