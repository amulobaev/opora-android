using Opora.ViewModels;
using Xamarin.Forms;

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
            BindingContext = new PillarsViewModel();
        }
    }
}