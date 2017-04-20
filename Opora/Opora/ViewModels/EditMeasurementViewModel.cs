using System;
using System.Globalization;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

using Opora.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using Opora.Domain;
using System.Linq;

namespace Opora.ViewModels
{
    public class EditMeasurementViewModel : PageViewModel
    {
        private string _height;
        private string _x;
        private string _h1;
        private string _h2;
        private string _result;

        private ICommand _saveCommand;
        private ICommand _calculateCommand;
        private readonly ObservableCollection<Pillar> _pillars = new ObservableCollection<Pillar>();
        private IRepository<Pillar, Guid> _repository;

        public EditMeasurementViewModel(IRepository<Pillar, Guid> repository)
        {
            _repository = repository;

            Title = "Замер";

            //Item = item;

            // TODO
            Height = X = H1 = H2 = Result = (0.0).ToString("F1");

            var pillars = _repository.GetItems().ToList();
            if (pillars.Any())
            {
                foreach (var item in pillars)
                {
                    _pillars.Add(item);
                }
            }
        }

        public Measurement Item { get; set; }

        public ObservableCollection<Pillar> Pillars
        {
            get { return _pillars; }
        }


        private Pillar _selectedPillar;

        public Pillar SelectedPillar
        {
            get { return _selectedPillar; }
            set
            {
                if (_selectedPillar == value)
                    return;
                _selectedPillar = value;
                RaisePropertyChanged();

                if (SelectedPillar != null)
                {
                    Height = SelectedPillar.Height.ToString();
                    X = SelectedPillar.Taper.ToString();
                }
            }
        }

        /// <summary>
        /// Высота опоры
        /// </summary>
        public string Height
        {
            get { return _height; }
            set { Set(() => Height, ref _height, value); }
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
            double height, h1, h2, x;
            if (!TryParse(Height, out height) || !TryParse(X, out x) || !TryParse(H1, out h1) || !TryParse(H2, out h2))
            {
                App.Current.MainPage.DisplayAlert("Расчёт", "Неверные исходные данные", "OK");
                return;
            }

            double result = x - Math.Abs(h1 - h2) * height;
            Result = result.ToString();
        }

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);
        }

        public override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.RaisePropertyChanged<T>(propertyExpression);
        }

    }
}