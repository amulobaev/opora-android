using Xamarin.Forms;

using Opora.ViewModels;

namespace Opora.Views
{
    public partial class PillarsPage : ContentPage
    {
        /// <summary>
        /// Конструктор
        /// </summary>
		public PillarsPage()
        {
            InitializeComponent();
            PageViewModel viewModel = App.Locator.Pillars;
            viewModel.Page = this;
            BindingContext = viewModel;
        }
    }
}