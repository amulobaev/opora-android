using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Opora.Models;

namespace Opora.Domain
{
    public class MeasurementRepository : IRepository<Measurement, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Pillar, Guid> _pillarRepository;
        private readonly IDatabase _database;

        public MeasurementRepository(IMapper mapper, IRepository<Pillar, Guid> pillarRepository, IDatabase database)
        {
            _mapper = mapper;
            _pillarRepository = pillarRepository;
            _database = database;
        }

        public void AddItem(Measurement item)
        {
            var entity = _mapper.Map<MeasurementEntity>(item);
            _database.AddItem(entity);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Measurement GetItem(Guid id)
        {
            var entity = _database.GetItem<MeasurementEntity>(id);
            if (entity == null)
                return null;

            var item = _mapper.Map<Measurement>(entity);
            item.Pillar = _pillarRepository.GetItem(entity.PillarId);
            return item;
        }

        public IEnumerable<Measurement> GetItems()
        {
            var items = new List<Measurement>();
            var entities = _database.GetItems<MeasurementEntity>().ToList();
            foreach (var entity in entities)
            {
                var item = _mapper.Map<Measurement>(entity);
                item.Pillar = _pillarRepository.GetItem(entity.PillarId);
                items.Add(item);
            }
            return items;
        }

        public void UpdateItem(Measurement item)
        {
            var entity = _mapper.Map<MeasurementEntity>(item);
            _database.UpdateItem(entity);
        }
    }
}