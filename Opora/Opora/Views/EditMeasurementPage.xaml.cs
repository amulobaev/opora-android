using Opora.Controls;

namespace Opora.Views
{
    public partial class EditMeasurementPage : CustomContentPage
	{
        public EditMeasurementPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.EditMeasurement;
        }
    }
}