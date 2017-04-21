using System;
using Xamarin.Forms;

namespace Opora.ViewModels
{
    public abstract class PageViewModel : BaseViewModel, IDisposable
    {
        public Page Page { get; set; }

        public virtual void Dispose()
        {
        }
    }
}