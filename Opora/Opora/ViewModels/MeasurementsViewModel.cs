using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Opora.Helpers;
using Opora.Models;
using Opora.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Opora.ViewModels
{
	public class MeasurementsViewModel : PageViewModel
	{
        /// <summary>
        /// Конструктор
        /// </summary>
		public MeasurementsViewModel(Page page) : base(page)
		{
			Title = "Замеры";
			Items = new ObservableCollection<Measurement>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<EditMeasurementViewModel, Measurement>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as Measurement;
				Items.Add(_item);
				await DataStore.AddItemAsync(_item);
			});

            ExecuteLoadItemsCommand();
        }

        public ObservableCollection<Measurement> Items { get; set; }

        private Measurement _selectedItem;

        public Measurement SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;
                RaisePropertyChanged();

                if (SelectedItem == null)
                    return;
                var page = new EditMeasurementPage();
                page.BindingContext = new EditMeasurementViewModel(page, SelectedItem);
                Page.Navigation.PushAsync(page);

                // Manually deselect item
                SelectedItem = null;
            }
        }

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

        private ICommand _addItemCommand;

        public ICommand AddItemCommand
        {
            get { return _addItemCommand ?? (_addItemCommand = new RelayCommand(AddItem)); }
        }

        private void AddItem()
        {
            Measurement item = new Measurement
            {
                //Id = Guid.NewGuid(),
            };
            var view = new EditMeasurementPage();
            var viewModel = new EditMeasurementViewModel(view, item);
            view.BindingContext = viewModel;

            Page.Navigation.PushAsync(view);
        }
    }
}