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
        private ICommand _addItemCommand;
        private Measurement _selectedItem;

        /// <summary>
        /// Конструктор
        /// </summary>
		public MeasurementsViewModel()
		{
			Title = "Замеры";
			Items = new ObservableCollection<Measurement>();
			//LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<EditMeasurementViewModel, Measurement>(this, "AddItem", async (obj, item) =>
			{
				var _item = item as Measurement;
				Items.Add(_item);
				//await DataStore.AddItemAsync(_item);
			});

            //ExecuteLoadItemsCommand();
        }

        public ObservableCollection<Measurement> Items { get; set; }

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
                // Здесь передать данные о замере
                Page.Navigation.PushAsync(page);

                // Manually deselect item
                SelectedItem = null;
            }
        }

        public Command LoadItemsCommand { get; set; }

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
            var page = new EditMeasurementPage();
            // Здесь передать данные о замере
            Page.Navigation.PushAsync(page);
        }
    }
}