using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Opora.Helpers;
using Opora.Models;
using Opora.Views;
using System.Collections.ObjectModel;

namespace Opora.ViewModels
{
	public class MeasurementsViewModel : BaseViewModel
	{
        /// <summary>
        /// Конструктор
        /// </summary>
		public MeasurementsViewModel()
		{
			Title = "Замеры";
			Items = new ObservableCollection<Measurement>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewItemPage, Measurement>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as Measurement;
				Items.Add(_item);
				await DataStore.AddItemAsync(_item);
			});
		}

        public ObservableCollection<Measurement> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();
				var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load items.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}