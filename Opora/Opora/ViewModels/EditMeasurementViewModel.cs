using System;
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
    public class EditMeasurementViewModel : EditorViewModel<Measurement>
    {
        private string _height;
        private string _x;
        private string _h1;
        private string _h2;
        private string _result;
        private Pillar _selectedPillar;
        private ICommand _calculateCommand;
        private readonly ObservableCollection<Pillar> _pillars = new ObservableCollection<Pillar>();
        private IRepository<Pillar, Guid> _repository;

        public EditMeasurementViewModel(IRepository<Pillar, Guid> repository)
        {
            _repository = repository;

            Title = "Замер";

            MessagingCenter.Subscribe<MeasurementsViewModel, Measurement>(this, "EditMeasurement", (obj, item) =>
            {
                Item = item;                
            });

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

        public ObservableCollection<Pillar> Pillars
        {
            get { return _pillars; }
        }

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

        public ICommand CalculateCommand
        {
            get { return _calculateCommand ?? (_calculateCommand = new RelayCommand(Calculate)); }
        }

        protected override void Save()
        {
            Item.Pillar = SelectedPillar;
            Item.UpdatedAt = DateTime.Now;

            MessagingCenter.Send(this, "AddItem", Item);
            Page.Navigation.PopToRootAsync();
        }

        private void Calculate()
        {
            double height, h1, h2, x;
            if (!Helpers.TryParse(Height, out height) || !Helpers.TryParse(X, out x) || !Helpers.TryParse(H1, out h1) || !Helpers.TryParse(H2, out h2))
            {
                Page.DisplayAlert("Расчёт", "Неверные исходные данные", "OK");
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