using AutoMapper;
using Ninject.Activation;

using Opora.Domain;
using Opora.Models;

namespace Opora
{
    public class MapperConfigurationProvider : Provider<IConfigurationProvider>
    {
        protected override IConfigurationProvider CreateInstance(IContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pillar, PillarEntity>();
                cfg.CreateMap<PillarEntity, Pillar>();
                cfg.CreateMap<Measurement, MeasurementEntity>();
                cfg.CreateMap<MeasurementEntity, Measurement>();
            });
            return config;
        }
    }
}