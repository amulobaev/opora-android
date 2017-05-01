using System;

namespace Opora.Domain
{
    public class MeasurementEntity : BaseEntity
    {
        public Guid PillarId { get; set; }

        public double Height { get; set; }

        public double Taper { get; set; }

        public double Measurement1 { get; set; }

        public double Measurement2 { get; set; }

        public string Location { get; set; }
    }
}