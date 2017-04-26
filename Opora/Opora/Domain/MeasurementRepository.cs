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
        private IRepository<Pillar, Guid> _pillarRepository;
        private readonly List<MeasurementEntity> _items;

        public MeasurementRepository(IMapper mapper, IRepository<Pillar, Guid> pillarRepository)
        {
            _mapper = mapper;
            _pillarRepository = pillarRepository;

            _items = new List<MeasurementEntity>();
        }

        public void AddItem(Measurement item)
        {
            var entity = _mapper.Map<MeasurementEntity>(item);
            _items.Add(entity);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Measurement GetItem(Guid id)
        {
            var entity = _items.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                var item = _mapper.Map<Measurement>(entity);
                item.Pillar = _pillarRepository.GetItem(entity.PillarId);                
                return item;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Measurement> GetItems()
        {
            var items = new List<Measurement>();
            foreach (var entity in _items)
            {
                var item = _mapper.Map<Measurement>(entity);
                item.Pillar = _pillarRepository.GetItem(entity.PillarId);
                items.Add(item);
            }
            return items;
        }

        public void UpdateItem(Measurement item)
        {
            var itemToUpdate = _items.FirstOrDefault(x => x.Id == item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.PillarId = item.Pillar.Id;
                itemToUpdate.Height = item.Height;
                itemToUpdate.Taper = item.Taper;
                itemToUpdate.Measurement1 = item.Measurement1;
                itemToUpdate.Measurement2 = item.Measurement2;
                itemToUpdate.UpdatedAt = item.UpdatedAt;
            }
        }
    }
}