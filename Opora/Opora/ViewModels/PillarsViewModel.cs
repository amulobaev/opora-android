using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Opora.Helpers;
using Opora.Models;
using Opora.Views;

namespace Opora.ViewModels
{
	public class PillarsViewModel : BaseViewModel
	{
        /// <summary>
        /// Конструктор
        /// </summary>
		public PillarsViewModel()
		{
			Title = "Опоры";
			Items = new ObservableRangeCollection<Measurement>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewItemPage, Measurement>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as Measurement;
				Items.Add(_item);
				await DataStore.AddItemAsync(_item);
			});
		}

        public ObservableRangeCollection<Measurement> Items { get; set; }

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
				Items.ReplaceRange(items);
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