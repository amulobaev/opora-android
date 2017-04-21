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
using Opora.Domain;
using System.Linq;

namespace Opora.ViewModels
{
	public class PillarsViewModel : PageViewModel
	{
        private IRepository<Pillar, Guid> _repository;
        private Pillar _selectedItem;
        private ICommand _addItemCommand;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PillarsViewModel(IRepository<Pillar, Guid> repository)
		{
            _repository = repository;

            Title = "Опоры";
			Items = new ObservableCollection<Pillar>();

            var items = _repository.GetItems().ToList();
            if (items.Any())
            {
                IsBusy = true;
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                IsBusy = false;
            }

            MessagingCenter.Subscribe<EditPillarViewModel, Pillar>(this, "AddItem", (obj, item) =>
			{
				var _item = item as Pillar;
				Items.Add(_item);
                //App.Current.MainPage.DisplayAlert("Test Title", "Test", "OK");
				//await DataStore.AddItemAsync(_item);
			});
		}

        public ObservableCollection<Pillar> Items { get; set; }

        public Command LoadItemsCommand { get; set; }

        public ICommand AddItemCommand
        {
            get { return _addItemCommand ?? (_addItemCommand = new RelayCommand(AddItem)); }
        }

        public Pillar SelectedItem
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
                var page = new EditPillarPage();
                page.BindingContext = new EditPillarViewModel();
                // Здесь передать данные об опоре
                Page.Navigation.PushAsync(page);

                // Manually deselect item
                SelectedItem = null;
            }
        }

        private void AddItem()
        {
            Page.Navigation.PushAsync(new EditPillarPage());

            Pillar pillar = new Pillar
            {
                //Id = Guid.NewGuid(),
                Name = "Новая опора"
            };
            MessagingCenter.Send(this, "EditPillar", pillar);
        }
    }
}