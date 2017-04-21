using System;
using Xamarin.Forms;
using Opora.ViewModels;

namespace Opora.Controls
{
    public abstract class CustomContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            PageViewModel viewModel = BindingContext as PageViewModel;
            if (viewModel != null && viewModel.Page == null)
                viewModel.Page = this;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IDisposable viewModel = BindingContext as IDisposable;
            if (viewModel != null)
                viewModel.Dispose();
        }
    }
}