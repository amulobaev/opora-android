using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Opora.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(Opora.Services.MockDataStore))]
namespace Opora.Services
{
    public class MockDataStore : IDataStore<Measurement>
    {
        bool isInitialized;
        List<Measurement> items;

        public async Task<bool> AddItemAsync(Measurement item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Measurement item)
        {
            await InitializeAsync();

            var _item = items.Where((Measurement arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Measurement item)
        {
            await InitializeAsync();

            var _item = items.Where((Measurement arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Measurement> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Measurement>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<Measurement>();
            var _items = new List<Measurement>
            {
                new Measurement { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
                new Measurement { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
                new Measurement { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
                new Measurement { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
                new Measurement { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
                new Measurement { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
            };

            foreach (var item in _items)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}
