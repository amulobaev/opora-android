using Opora.Models;
using System.Globalization;

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
			set { Set(() => Quantity, ref quantity, value); }
		}

        private bool TryParse(string s, out double result)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            s = s.Replace(".", separator).Replace(",", separator);
            return double.TryParse(s, out result);
        }

    }
}