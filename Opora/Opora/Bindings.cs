using System;
using System.Collections.Generic;
using System.Text;

using Ninject.Modules;
using Opora.ViewModels;

namespace Opora
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MeasurementsViewModel>().ToSelf();
        }
    }
}