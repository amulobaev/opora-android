using System;
using Xamarin.Forms;

namespace Opora.Controls
{
    public abstract class CustomContentPage : ContentPage
    {
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IDisposable disposable = BindingContext as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}