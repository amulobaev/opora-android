using Opora.Controls;

namespace Opora.Views
{
    public partial class PillarsPage : CustomContentPage
    {
		public PillarsPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.Pillars;
        }
    }
}