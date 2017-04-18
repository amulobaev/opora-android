using System;

using Xamarin.Forms;

using Opora.Models;
using Opora.ViewModels;

namespace Opora.Views
{
	public partial class PillarsPage : ContentPage
	{
		PillarsViewModel viewModel;

        /// <summary>
        /// Конструктор
        /// </summary>
		public PillarsPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new PillarsViewModel();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Pillar;
			if (item == null)
				return;

            var page = new EditPillarPage(new EditPillarViewModel(item));
            await Navigation.PushAsync(page);

			// Manually deselect item
			ItemsListView.SelectedItem = null;
		}

        /// <summary>
        /// Добавление нового замера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		async void AddItem_Clicked(object sender, EventArgs e)
		{
            Pillar newItem = new Pillar
            {
                //Id = Guid.NewGuid(),

            };
            var view = new EditPillarPage(new EditPillarViewModel(newItem));
            await Navigation.PushAsync(view);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Items.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}
	}
}