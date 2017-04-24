using System;
using System.Collections.Generic;

using Opora.Models;
using System.Linq;
using AutoMapper;

namespace Opora.Domain
{
    public class PillarRepository : IRepository<Pillar, Guid>
    {
        private readonly List<PillarEntity> _items;

        static PillarRepository()
        {
            try
            {
                Mapper.Initialize(x => x.CreateMap<PillarEntity, Pillar>());
                Mapper.Initialize(x => x.CreateMap<Pillar, PillarEntity>());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PillarRepository()
        {
            _items = new List<PillarEntity>()
            {
                new PillarEntity {Name = "СС 156.6", Height = 15.6, Taper = 11.7 },
                new PillarEntity {Name = "СС 136.6", Height = 13.6, Taper = 10.1 }
            };
        }

        public void AddItem(Pillar item)
        {
            var entity = Mapper.Map<PillarEntity>(item);
            _items.Add(entity);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Pillar GetItem(Guid id)
        {
            var entity = _items.FirstOrDefault(x => x.Id == id);
            return entity != null ? Mapper.Map<Pillar>(entity) : null;
        }

        public IEnumerable<Pillar> GetItems()
        {
            try
            {
                var items = _items.Select(x => Mapper.Map<PillarEntity, Pillar>(x)).ToList();
                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
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