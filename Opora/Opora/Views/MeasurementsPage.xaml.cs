using System;

using Xamarin.Forms;

using Opora.Models;
using Opora.ViewModels;

namespace Opora.Views
{
	public partial class MeasurementsPage : ContentPage
	{
        MeasurementsViewModel viewModel;

		public MeasurementsPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new MeasurementsViewModel(this);
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Measurement;
			if (item == null)
				return;

            var view = new EditMeasurementPage();
            var viewModel = new EditMeasurementViewModel(view, item);
            view.BindingContext = viewModel;

            await Navigation.PushAsync(view);

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
            Measurement item = new Measurement
            {
                //Id = Guid.NewGuid(),
            };
            var view = new EditMeasurementPage();
            var viewModel = new EditMeasurementViewModel(view, item);
            view.BindingContext = viewModel;

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