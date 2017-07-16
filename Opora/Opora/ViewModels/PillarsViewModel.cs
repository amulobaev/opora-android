using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

using Opora.Domain;
using Opora.Models;
using Opora.Views;

namespace Opora.ViewModels
{
    public class PillarsViewModel : PageViewModel
	{
        private IRepository<Pillar, Guid> _pillarRepository;
        private Pillar _selectedItem;
        private ICommand _addItemCommand;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PillarsViewModel(IRepository<Pillar, Guid> pillarRepository)
		{
            _pillarRepository = pillarRepository;

            Title = "Опоры";
			Items = new ObservableCollection<Pillar>();

            Update();

		    MessagingCenter.Subscribe<EditPillarViewModel>(this, "UpdatePillars", obj =>
			{
                Update();
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
                Page.Navigation.PushAsync(new EditPillarPage());
                MessagingCenter.Send(this, "EditPillar", SelectedItem);

                // Manually deselect item
                SelectedItem = null;
            }
        }

	    private void Update()
	    {
	        IsBusy = true;

            Items.Clear();

            var items = _pillarRepository.GetItems().ToList();
	        if (items.Any())
	        {
	            foreach (var item in items)
	            {
	                Items.Add(item);
	            }
	        }

	        IsBusy = false;
	    }

        private void AddItem()
        {
            Page.Navigation.PushAsync(new EditPillarPage());
            DateTime now = DateTime.Now;
            Pillar pillar = new Pillar
            {
                Id = Guid.NewGuid(),
                CreatedAt = now,
                UpdatedAt = now,
                Name = "Новая опора"
            };
            MessagingCenter.Send(this, "EditPillar", pillar);
        }
    }
}