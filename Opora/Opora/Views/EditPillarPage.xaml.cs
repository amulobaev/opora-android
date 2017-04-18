using System;
using Xamarin.Forms;

using Opora.ViewModels;
using Opora.Models;

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
            Pillar pillar = new Pillar { Name = viewModel.Name };
            MessagingCenter.Send(this, "AddItem", pillar);
            await Navigation.PopToRootAsync();
        }
    }
}