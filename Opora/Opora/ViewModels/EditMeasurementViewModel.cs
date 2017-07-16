using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

using Opora.Models;
using Opora.Domain;
using Plugin.Geolocator;

namespace Opora.ViewModels
{
    public class EditMeasurementViewModel : EditorViewModel<Measurement>
    {
        private string _height;
        private string _taper;
        private string _measurement1;
        private string _measurement2;
        private string _position;
        private double _angle;
        private Pillar _selectedPillar;
        private ICommand _calculateCommand;
        private readonly ObservableCollection<Pillar> _pillars = new ObservableCollection<Pillar>();
        private readonly IRepository<Pillar, Guid> _pillarRepository;
        private readonly IRepository<Measurement, Guid> _measurementRepository;
        private string _warning;
        private ICommand _getPositionCommand;

        public EditMeasurementViewModel(IRepository<Pillar, Guid> pillarRepository, IRepository<Measurement, Guid> measurementRepository)
        {
            _pillarRepository = pillarRepository;
            _measurementRepository = measurementRepository;

            Title = "Замер";

            // TODO
            Height = Taper = Measurement1 = Measurement2 = (0.0).ToString("F1");
            Angle = 0.0;

            var pillars = _pillarRepository.GetItems().ToList();
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
                Angle = item.Angle;
                Position = item.Position;
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

        public string Position
        {
            get { return _position; }
            set { Set(() => Position, ref _position, value); }
        }

        public double Angle
        {
            get { return _angle; }
            set { Set(() => Angle, ref _angle, value); }
        }

        public ICommand CalculateCommand
        {
            get { return _calculateCommand ?? (_calculateCommand = new RelayCommand(Calculate)); }
        }

        public ICommand GetPositionCommand
        {
            get { return _getPositionCommand ?? (_getPositionCommand = new RelayCommand(GetPosition)); }
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
                DisplayAlert("Не указана марка опоры");
                return;
            }
            double height;
            if (!Helpers.TryParse(Height, out height))
            {
                DisplayAlert("Высота опоры указана неверно");
                return;
            }
            double taper;
            if (!Helpers.TryParse(Taper, out taper))
            {
                DisplayAlert("Конусность опоры указана неверно");
                return;
            }
            double measurement1;
            if (!Helpers.TryParse(Measurement1, out measurement1))
            {
                DisplayAlert("Первое измерение указано неверно");
                return;
            }
            double measurement2;
            if (!Helpers.TryParse(Measurement2, out measurement2))
            {
                DisplayAlert("Второе измерение указано неверно");
                return;
            }

            Item.Pillar = SelectedPillar;
            Item.Height = height;
            Item.Taper = taper;
            Item.Measurement1 = measurement1;
            Item.Measurement2 = measurement2;
            Item.UpdatedAt = DateTime.Now;
            Item.Angle = Angle;
            Item.Position = Position;

            // Сохранение в базе
            if (_measurementRepository.GetItem(Item.Id) == null)
            {
                _measurementRepository.AddItem(Item);
            }
            else
            {
                _measurementRepository.UpdateItem(Item);
            }

            MessagingCenter.Send(this, "UpdateMeasurements");
            Page.Navigation.PopToRootAsync();
        }

        private void Calculate()
        {
            double height, taper, measurement1, measurement2;
            if (!Helpers.TryParse(Height, out height) || !Helpers.TryParse(Taper, out taper) || !Helpers.TryParse(Measurement1, out measurement1) || !Helpers.TryParse(Measurement2, out measurement2))
            {
                DisplayAlert("Неверные исходные данные");
                return;
            }

            Angle = taper - Math.Abs(measurement1 - measurement2) * height;
            Warning = Angle > 12 ? "Требуется выправка или замена опоры контактной сети" : string.Empty;
        }

        private async void GetPosition()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 10;

                var position = await locator.GetPositionAsync(10000);
                Position = string.Format("Ш {0:0.00000}, Д {1:0.00000}", position.Latitude, position.Longitude);
            }
            catch
            {
                DisplayAlert("Ошибка при определении местоположения");
            }
        }
    }
}