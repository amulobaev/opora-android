using Opora.Controls;
using Opora.ViewModels;

namespace Opora.Views
{
    public partial class EditPillarPage : CustomContentPage
	{
        public EditPillarPage()
        {
            InitializeComponent();
            PageViewModel viewModel = App.Locator.EditPillar;
            viewModel.Page = this;
            BindingContext = viewModel;
        }
    }
}