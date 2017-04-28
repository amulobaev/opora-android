using System;
using System.Collections.Generic;

using Opora.Models;
using System.Linq;
using AutoMapper;

namespace Opora.Domain
{
    public class PillarRepository : IRepository<Pillar, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IDatabase _database;

        public PillarRepository(IMapper mapper, IDatabase database)
        {
            _mapper = mapper;
            _database = database;

            if (_database.GetItems<PillarEntity>().Count() == 0)
            {
                var now = DateTime.Now;
                var items = new List<PillarEntity>()
                {
                    new PillarEntity {Id = new Guid("{6EB14D3E-BC5C-4FFD-B075-CDD5BA3147EA}"),Name = "СС 156.6", Height = 15.6, Taper = 11.7 },
                    new PillarEntity {Id = new Guid("{724D318D-F187-45CB-8A47-6C2BAA2C94AF}"),Name = "СС 136.6", Height = 13.6, Taper = 10.1 },
                    new PillarEntity {Id = new Guid("{40D43912-52E5-4E51-BB5C-947850F81BEE}"),Name = "СО 108.6", Height = 10.8, Taper = 8 },
                    new PillarEntity {Id = new Guid("{3DBD4C81-F93A-402E-BC43-BE9B23A0CE03}"),Name = "ССА-100.6-3", Height = 10, Taper = 7.25 },
                    new PillarEntity {Id = new Guid("{4B77D78E-B8E6-4A98-BCD4-1503B0FB36CA}"),Name = "СП 104.6", Height = 10.4, Taper = 7.75 },
                    new PillarEntity {Id = new Guid("{4E15B6E4-62E4-4484-A47F-66ABD9B51662}"),Name = "М1-10-60", Height = 9.6, Taper = 12 },
                    new PillarEntity {Id = new Guid("{F9C4AAC0-DBDA-47FB-9064-467C1F7BDB31}"),Name = "МГК1-12-100С", Height = 12, Taper = 0 },
                    new PillarEntity {Id = new Guid("{7C14A11E-A680-47DB-80A6-FAC0B46E1757}"),Name = "МШ-10-60", Height = 12, Taper = 15 },
                };
                foreach (var item in items)
                {
                    _database.AddItem(item);
                }
            }
        }

        public void AddItem(Pillar item)
        {
            var entity = _mapper.Map<PillarEntity>(item);
            _database.AddItem(entity);
            //_items.Add(entity);
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Pillar GetItem(Guid id)
        {
            //var entity = _items.FirstOrDefault(x => x.Id == id);
            var entity = _database.GetItem<PillarEntity>(id);
            return entity != null ? _mapper.Map<Pillar>(entity) : null;
        }

        public IEnumerable<Pillar> GetItems()
        {
            var entities = _database.GetItems<PillarEntity>().ToList();
            var items = _mapper.Map<IEnumerable<PillarEntity>, List<Pillar>>(entities);
            return items;
        }

        public void UpdateItem(Pillar item)
        {
            //var itemToUpdate = _items.FirstOrDefault(x => x.Id == item.Id);
            //if (itemToUpdate != null)
            //{
            //    itemToUpdate.Name = item.Name;
            //    itemToUpdate.Height = item.Height;
            //    itemToUpdate.Taper = item.Taper;
            //    itemToUpdate.UpdatedAt = item.UpdatedAt;
            //}
        }
    }
}