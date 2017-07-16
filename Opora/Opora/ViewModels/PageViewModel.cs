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

        public void DisplayAlert(string message)
        {
            if (Page == null)
                return;

            Page.DisplayAlert("Замер", message, "OK");
        }
    }
}