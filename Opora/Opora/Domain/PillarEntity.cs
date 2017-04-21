using System;

namespace Opora.Domain
{
    public class PillarEntity : BaseEntity
    {
        public string Name { get; set; }

        public double Height { get; set; }

        public double Taper { get; set; }
    }
}