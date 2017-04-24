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
        public double H1 { get; set; }

        /// <summary>
        /// Второе измерение
        /// </summary>
        public double H2 { get; set; }
    }
}