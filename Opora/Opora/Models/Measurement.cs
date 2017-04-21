using System;

namespace Opora.Models
{
    /// <summary>
    /// Замер
    /// </summary>
    public class Measurement : BaseModel
    {
        /// <summary>
        /// Высота опоры
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Конусность опоры
        /// </summary>
        public double X { get; set; }

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