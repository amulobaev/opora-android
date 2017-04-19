using System;

namespace Opora.Models
{
    /// <summary>
    /// Замер
    /// </summary>
    public class Measurement : BaseDataObject
    {
        string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { Set(() => Text, ref text, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { Set(() => Description, ref description, value); }
        }

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