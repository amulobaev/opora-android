using Xamarin.Forms;

using Opora.ViewModels;

namespace Opora.Views
{
	public partial class EditMeasurementPage : ContentPage
	{
        public EditMeasurementPage()
        {
            InitializeComponent();
            PageViewModel viewModel = App.Locator.EditMeasurement;
            viewModel.Page = this;
            BindingContext = viewModel;
        }
    }
}