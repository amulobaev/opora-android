using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

using Opora.Models;
using Opora.Views;
using System;

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

			MessagingCenter.Subscribe<EditMeasurementViewModel, Measurement>(this, "AddItem", (obj, item) =>
			{
				var _item = item as Measurement;
				Items.Add(_item);
			});
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
                // Здесь передать данные о замере
                Page.Navigation.PushAsync(new EditMeasurementPage());
                MessagingCenter.Send(this, "EditMeasurement", SelectedItem);

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
            Page.Navigation.PushAsync(new EditMeasurementPage());

            DateTime now = DateTime.Now;
            var measurement = new Measurement
            {
                Id = Guid.NewGuid(),
                CreatedAt = now,
                UpdatedAt = now,
            };

            MessagingCenter.Send(this, "EditMeasurement", measurement);
        }
    }
}