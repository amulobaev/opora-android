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
	public class PillarsViewModel : BaseViewModel
	{
        public ObservableCollection<Pillar> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
		public PillarsViewModel()
		{
			Title = "Опоры";
			Items = new ObservableCollection<Pillar>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            Items.Add(new Pillar { Name = "Опора 1", Height = 1, Taper = 2 });

            MessagingCenter.Subscribe<EditPillarPage, Pillar>(this, "AddItem", (obj, item) =>
			{
				var _item = item as Pillar;
				Items.Add(_item);
                //App.Current.MainPage.DisplayAlert("Test Title", "Test", "OK");
				//await DataStore.AddItemAsync(_item);
			});
		}

        async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();
				//var items = await DataStore.GetItemsAsync(true);
				//Items.ReplaceRange(items);
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