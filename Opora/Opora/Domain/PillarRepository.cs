using System;
using System.Collections.Generic;

using Opora.Models;
using System.Linq;
using AutoMapper;

namespace Opora.Domain
{
    public class PillarRepository : IRepository<Pillar, Guid>
    {
        private IMapper _mapper;

        private readonly List<PillarEntity> _items;

        public PillarRepository(IMapper mapper)
        {
            _mapper = mapper;

            _items = new List<PillarEntity>()
            {
                new PillarEntity {Name = "СС 156.6", Height = 15.6, Taper = 11.7 },
                new PillarEntity {Name = "СС 136.6", Height = 13.6, Taper = 10.1 },
                new PillarEntity {Name = "СО 108.6", Height = 10.8, Taper = 8 },
                new PillarEntity {Name = "ССА-100.6-3", Height = 10, Taper = 7.25 },
                new PillarEntity {Name = "СП 104.6", Height = 10.4, Taper = 7.75 },
                new PillarEntity {Name = "М1-10-60", Height = 9.6, Taper = 12 },
                new PillarEntity {Name = "МГК1-12-100С", Height = 12, Taper = 0 },
                new PillarEntity {Name = "МШ-10-60", Height = 12, Taper = 15 },
            };
        }

        public void AddItem(Pillar item)
        {
            var entity = _mapper.Map<PillarEntity>(item);
            _items.Add(entity);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Pillar GetItem(Guid id)
        {
            var entity = _items.FirstOrDefault(x => x.Id == id);
            return entity != null ? _mapper.Map<Pillar>(entity) : null;
        }

        public IEnumerable<Pillar> GetItems()
        {
            var items = _mapper.Map<IEnumerable<PillarEntity>, List<Pillar>>(_items);
            return items;
        }

        public void UpdateItem(Pillar item)
        {
            var itemToUpdate = _items.FirstOrDefault(x => x.Id == item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Name = item.Name;
                itemToUpdate.Height = item.Height;
                itemToUpdate.Taper = item.Taper;
                itemToUpdate.UpdatedAt = item.UpdatedAt;
            }
        }
    }
}