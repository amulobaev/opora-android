using AutoMapper;
using Ninject.Activation;

using Opora.Domain;
using Opora.Models;

namespace Opora
{
    public class AutoMapperConfigurationProvider : Provider<IConfigurationProvider>
    {
        protected override IConfigurationProvider CreateInstance(IContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pillar, PillarEntity>();
                cfg.CreateMap<PillarEntity, Pillar>();
            });
            return config;
        }
    }
}