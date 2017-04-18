using Opora.Models;

namespace Opora.ViewModels
{
	public class EditMeasurementViewModel : BaseViewModel
	{
        int quantity = 1;

        public EditMeasurementViewModel(Measurement item = null)
		{
			Title = "Замер";
			Item = item;
		}

        public Measurement Item { get; set; }

        public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}