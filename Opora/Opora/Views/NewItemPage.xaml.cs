using System;

using Xamarin.Forms;

using Opora.Models;

namespace Opora.Views
{
	public partial class NewItemPage : ContentPage
	{
		public Measurement Item { get; set; }

		public NewItemPage()
		{
			InitializeComponent();

			Item = new Measurement
            {
				Text = "Item name",
				Description = "This is a nice description"
			};

			BindingContext = this;
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddItem", Item);
			await Navigation.PopToRootAsync();
		}
	}
}