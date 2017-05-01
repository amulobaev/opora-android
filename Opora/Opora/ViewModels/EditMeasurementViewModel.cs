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
        private string _taper;
        private string _measurement1;
        private string _measurement2;
        private string _location;
        private string _result;
        private Pillar _selectedPillar;
        private ICommand _calculateCommand;
        private readonly ObservableCollection<Pillar> _pillars = new ObservableCollection<Pillar>();
        private IRepository<Pillar, Guid> _repository;
        private string _warning;

        public EditMeasurementViewModel(IRepository<Pillar, Guid> repository)
        {
            _repository = repository;

            Title = "Замер";

            // TODO
            Height = Taper = Measurement1 = Measurement2 = Result = (0.0).ToString("F1");

            var pillars = _repository.GetItems().ToList();
            if (pillars.Any())
            {
                foreach (var item in pillars)
                {
                    _pillars.Add(item);
                }
            }

            MessagingCenter.Subscribe<MeasurementsViewModel, Measurement>(this, "EditMeasurement", (obj, item) =>
            {
                Item = item;
                SelectedPillar = item.Pillar != null ? _pillars.FirstOrDefault(x => x.Id == item.Pillar.Id) : null;
                Height = item.Height.ToString();
                Taper = item.Taper.ToString();
                Measurement1 = item.Measurement1.ToString();
                Measurement2 = item.Measurement2.ToString();
                Location = item.Location;
            });
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
                    Taper = SelectedPillar.Taper.ToString();
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

        public string Taper
        {
            get { return _taper; }
            set { Set(() => Taper, ref _taper, value); }
        }

        public string Measurement1
        {
            get { return _measurement1; }
            set { Set(() => Measurement1, ref _measurement1, value); }
        }

        public string Measurement2
        {
            get { return _measurement2; }
            set { Set(() => Measurement2, ref _measurement2, value); }
        }

        public string Location
        {
            get { return _location; }
            set { Set(() => Location, ref _location, value); }
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

        public string Warning
        {
            get { return _warning; }
            set { Set(() => Warning, ref _warning, value); }
        }

        protected override void Save()
        {
            if (SelectedPillar == null)
            {
                Page.DisplayAlert("Замер", "Не указана марка опоры", "OK");
                return;
            }
            double height;
            if (!Helpers.TryParse(Height, out height))
            {
                Page.DisplayAlert("Замер", "Высота опоры указана неверно", "OK");
                return;
            }
            double taper;
            if (!Helpers.TryParse(Taper, out taper))
            {
                Page.DisplayAlert("Замер", "Конусность опоры указана неверно", "OK");
                return;
            }
            double measurement1;
            if (!Helpers.TryParse(Measurement1, out measurement1))
            {
                Page.DisplayAlert("Замер", "Первое измерение указано неверно", "OK");
                return;
            }
            double measurement2;
            if (!Helpers.TryParse(Measurement2, out measurement2))
            {
                Page.DisplayAlert("Замер", "Второе измерение указано неверно", "OK");
                return;
            }

            Item.Pillar = SelectedPillar;
            Item.Height = height;
            Item.Taper = taper;
            Item.Measurement1 = measurement1;
            Item.Measurement2 = measurement2;
            Item.UpdatedAt = DateTime.Now;

            MessagingCenter.Send(this, "AddItem", Item);
            Page.Navigation.PopToRootAsync();
        }

        private void Calculate()
        {
            double height, taper, measurement1, measurement2;
            if (!Helpers.TryParse(Height, out height) || !Helpers.TryParse(Taper, out taper) || !Helpers.TryParse(Measurement1, out measurement1) || !Helpers.TryParse(Measurement2, out measurement2))
            {
                Page.DisplayAlert("Расчёт", "Неверные исходные данные", "OK");
                return;
            }

            double result = taper - Math.Abs(measurement1 - measurement2) * height;
            Result = result.ToString();
            Warning = result > 12 ? "Требуется выправка или замена опоры контактной сети" : string.Empty;
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