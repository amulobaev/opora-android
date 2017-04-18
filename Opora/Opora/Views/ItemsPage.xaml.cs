﻿using System;

using Xamarin.Forms;

using Opora.Models;
using Opora.ViewModels;

namespace Opora.Views
{
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;

		public ItemsPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new ItemsViewModel();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Measurement;
			if (item == null)
				return;

			await Navigation.PushAsync(new ItemDetailPage(new EditMeasureViewModel(item)));

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
            Measurement newItem = new Measurement
            {
                //Id = Guid.NewGuid(),
            };
            var view = new ItemDetailPage(new EditMeasureViewModel(newItem));
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