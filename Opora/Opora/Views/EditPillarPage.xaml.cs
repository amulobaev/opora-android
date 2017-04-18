using System;
using Xamarin.Forms;

using Opora.ViewModels;

namespace Opora.Views
{
	public partial class EditPillarPage : ContentPage
	{
        EditPillarViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public EditPillarPage()
        {
            InitializeComponent();
        }

        public EditPillarPage(EditPillarViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = this.viewModel = viewModel;
		}

        async void Save_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}