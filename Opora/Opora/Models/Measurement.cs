using System;

namespace Opora.Models
{
    /// <summary>
    /// Замер
    /// </summary>
    public class Measurement : BaseModel
    {
        private double _height;
        private double _taper;
        private double _measurement1;
        private double _measurement2;
        private string _location;

        public Pillar Pillar { get; set; }

        /// <summary>
        /// Высота опоры
        /// </summary>
        public double Height
        {
            get { return _height; }
            set { Set(() => Height, ref _height, value); }
        }

        /// <summary>
        /// Конусность опоры
        /// </summary>
        public double Taper
        {
            get { return _taper; }
            set { Set(() => Taper, ref _taper, value); }
        }

        /// <summary>
        /// Первое измерение
        /// </summary>
        public double Measurement1
        {
            get { return _measurement1; }
            set { Set(() => Measurement1, ref _measurement1, value); }
        }

        /// <summary>
        /// Второе измерение
        /// </summary>
        public double Measurement2
        {
            get { return _measurement2; }
            set { Set(() => Measurement2, ref _measurement2, value); }
        }

        public string Location
        {
            get { return _location; }
            set { Set(() => Location, ref _location, value); }
        }
    }
}