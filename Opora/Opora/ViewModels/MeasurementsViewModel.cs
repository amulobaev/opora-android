using System.Collections.ObjectModel;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

using Opora.Models;
using Opora.Views;
using System;
using Opora.Domain;
using System.Linq;

namespace Opora.ViewModels
{
    public class MeasurementsViewModel : PageViewModel
	{
        private ICommand _addItemCommand;
        private Measurement _selectedItem;
        private IRepository<Measurement, Guid> _repository;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MeasurementsViewModel(IRepository<Measurement, Guid> repository)
		{
            _repository = repository;

            Title = "Замеры";
			Items = new ObservableCollection<Measurement>();

			MessagingCenter.Subscribe<EditMeasurementViewModel, Measurement>(this, "AddItem", (obj, item) =>
			{
                if (Items.Any(x => x.Id == item.Id))
                {
                    _repository.UpdateItem(item);
                }
                else
                {
                    Items.Add(item);
                    _repository.AddItem(item);
                }
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