using System;
using System.Globalization;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

using Opora.Models;

namespace Opora.ViewModels
{
	public class EditMeasurementViewModel : PageViewModel
	{
        int quantity = 1;
        private ICommand _saveCommand;

        public EditMeasurementViewModel(Page page, Measurement item = null) : base(page)
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

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        private bool TryParse(string s, out double result)
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            s = s.Replace(".", separator).Replace(",", separator);
            return double.TryParse(s, out result);
        }

        private void Save()
        {
            MessagingCenter.Send(this, "AddItem", Item);
            Page.Navigation.PopToRootAsync();
        }
    }
}