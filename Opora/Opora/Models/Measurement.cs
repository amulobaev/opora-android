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
        private double _angle;
        private string _position;

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

        /// <summary>
        /// Угол наклона
        /// </summary>
        public double Angle
        {
            get { return _angle; }
            set { Set(() => Angle, ref _angle, value); }
        }

        public string Position
        {
            get { return _position; }
            set { Set(() => Position, ref _position, value); }
        }
    }
}