using Opora.Controls;

namespace Opora.Views
{
    public partial class MeasurementsPage : CustomContentPage
	{
		public MeasurementsPage()
		{
			InitializeComponent();
            BindingContext = App.Locator.Measurements;
		}
	}
}