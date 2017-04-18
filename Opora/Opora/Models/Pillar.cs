using System;

namespace Opora.Models
{
    /// <summary>
    /// Опора
    /// </summary>
    public class Pillar : BaseDataObject
    {
        /// <summary>
        /// Марка опоры
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Высота опоры
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Конусность опоры
        /// </summary>
        public double Taper { get; set; }
    }
}