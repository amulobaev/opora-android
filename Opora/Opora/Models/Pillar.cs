using System;

namespace Opora.Models
{
    /// <summary>
    /// Опора
    /// </summary>
    public class Pillar : BaseModel
    {
        private string _name;
        private double _height;
        private double _taper;

        /// <summary>
        /// Марка опоры
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

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

        public override string ToString()
        {
            return Name;
        }
    }
}