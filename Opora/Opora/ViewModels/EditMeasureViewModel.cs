using Opora.Models;

namespace Opora.ViewModels
{
	public class EditMeasureViewModel : BaseViewModel
	{
        int quantity = 1;

        public EditMeasureViewModel(Measurement item = null)
		{
			Title = item.Text;
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