using Xamarin.Forms;

using Opora.ViewModels;

namespace Opora.Views
{
	public partial class MeasurementsPage : ContentPage
	{
		public MeasurementsPage()
		{
			InitializeComponent();
            PageViewModel viewModel = App.Locator.Measurements;
            viewModel.Page = this;
            BindingContext = viewModel;
		}
	}
}