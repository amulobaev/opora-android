using System;
using Xamarin.Forms;

using Opora.ViewModels;
using Opora.Models;

namespace Opora.Views
{
	public partial class EditMeasurementPage : ContentPage
	{
        public Measurement Item { get; set; }

        EditMeasurementViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EditMeasurementPage()
        {
            InitializeComponent();
        }

        public EditMeasurementPage(EditMeasurementViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}