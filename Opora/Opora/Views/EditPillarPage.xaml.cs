using Opora.Controls;

namespace Opora.Views
{
    public partial class EditPillarPage : CustomContentPage
	{
        public EditPillarPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.EditPillar;
        }
    }
}