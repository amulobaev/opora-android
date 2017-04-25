using System;

using AutoMapper;
using Ninject;
using Ninject.Modules;

using Opora.Domain;
using Opora.Models;
using Opora.ViewModels;

namespace Opora
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MeasurementsViewModel>().ToSelf();
            Bind<PillarsViewModel>().ToSelf();
            Bind<IRepository<Measurement, Guid>>().To<MeasurementRepository>().InSingletonScope();
            Bind<IRepository<Pillar, Guid>>().To<PillarRepository>().InSingletonScope();
            Bind<IConfigurationProvider>().ToProvider<MapperConfigurationProvider>().InSingletonScope();
            Bind<IMapper>().ToMethod(context => context.Kernel.Get<IConfigurationProvider>().CreateMapper());
        }
    }
}