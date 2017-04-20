using Ninject;

using Opora.ViewModels;

namespace Opora
{
    public class ViewModelLocator
    {
        private readonly StandardKernel _kernel;

        public ViewModelLocator()
        {
            _kernel = new StandardKernel(new Bindings());
        }

        public MeasurementsViewModel Measurements
        {
            get { return _kernel.Get<MeasurementsViewModel>(); }
        }
    }
}