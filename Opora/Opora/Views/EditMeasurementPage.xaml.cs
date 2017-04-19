using System;

using Xamarin.Forms;

using Opora.ViewModels;
using Opora.Models;

namespace Opora.Views
{
	public partial class EditMeasurementPage : ContentPage
	{
        public Measurement Item { get; set; }

        private EditMeasurementViewModel _viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EditMeasurementPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditMeasurementViewModel(this);
        }

        public EditMeasurementPage(EditMeasurementViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = _viewModel = viewModel;
		}
    }
}