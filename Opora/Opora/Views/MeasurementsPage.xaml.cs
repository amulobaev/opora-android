using Opora.ViewModels;
using Xamarin.Forms;

namespace Opora.Views
{
	public partial class MeasurementsPage : ContentPage
	{
		public MeasurementsPage()
		{
			InitializeComponent();
            BindingContext = new MeasurementsViewModel();
		}
	}
}