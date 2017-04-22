using System;
using System.Windows.Input;

using Xamarin.Forms;
using GalaSoft.MvvmLight.Command;

using Opora.Models;

namespace Opora.ViewModels
{
    /// <summary>
    /// Модель представления создания/изменения опоры
    /// </summary>
    public class EditPillarViewModel : EditorViewModel<Pillar>
    {
        private string _name;
        private string _height;
        private string _taper;

        public EditPillarViewModel()
        {
            Title = "Опора";

            MessagingCenter.Subscribe<PillarsViewModel, Pillar>(this, "EditPillar", (obj, item) =>
            {
                Item = item;
                Name = item.Name;
                Height = item.Height.ToString();
                Taper = item.Taper.ToString();
            });
        }

        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        public string Height
        {
            get { return _height; }
            set { Set(() => Height, ref _height, value); }
        }

        public string Taper
        {
            get { return _taper; }
            set { Set(() => Taper, ref _taper, value); }
        }

        public override void Dispose()
        {
            MessagingCenter.Unsubscribe<PillarsViewModel, Pillar>(this, "EditPillar");
            base.Dispose();
        }

        protected override void Save()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Page.DisplayAlert("Опора", "Не указана марка опоры", "OK");
                return;
            }
            double height;
            if (!Helpers.TryParse(Height, out height))
            {
                Page.DisplayAlert("Опора", "Высота опоры указана неверно", "OK");
                return;
            }
            double taper;
            if (!Helpers.TryParse(Taper, out taper))
            {
                Page.DisplayAlert("Опора", "Конусность опоры указана неверно", "OK");
                return;
            }

            Item.Name = Name;
            Item.Height = height;
            Item.Taper = taper;
            Item.UpdatedAt = DateTime.Now;

            MessagingCenter.Send(this, "AddItem", Item);
            Page.Navigation.PopToRootAsync();
        }
    }
}