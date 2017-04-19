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
        private string _h;
        private string _x;
        private string _h1;
        private string _h2;
        private string _result;

        private ICommand _saveCommand;
        private ICommand _calculateCommand;

        public EditMeasurementViewModel(Page page, Measurement item) : base(page)
        {
            Title = "Замер";

            Item = item;

            // TODO
            H = X = H1 = H2 = Result = (0.0).ToString("F1");
        }

        public Measurement Item { get; set; }

        public string H
        {
            get { return _h; }
            set { Set(() => H, ref _h, value); }
        }

        public string X
        {
            get { return _x; }
            set { Set(() => X, ref _x, value); }
        }

        public string H1
        {
            get { return _h1; }
            set { Set(() => H1, ref _h1, value); }
        }

        public string H2
        {
            get { return _h2; }
            set { Set(() => H2, ref _h2, value); }
        }

        public string Result
        {
            get { return _result; }
            set { Set(() => Result, ref _result, value); }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        public ICommand CalculateCommand
        {
            get { return _calculateCommand ?? (_calculateCommand = new RelayCommand(Calculate)); }
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

        private void Calculate()
        {
            double h, h1, h2, x;
            if (!TryParse(H, out h) || !TryParse(X, out x) || !TryParse(H1, out h1) || !TryParse(H2, out h2))
            {
                App.Current.MainPage.DisplayAlert("Расчёт", "Неверные исходные данные", "OK");
                return;
            }

            double result = x - Math.Abs(h1 - h2) * h;
            Result = result.ToString();
        }

    }
}