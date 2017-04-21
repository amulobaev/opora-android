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
            Pillar pillar = new Pillar { Name = Name };
            MessagingCenter.Send(this, "AddItem", pillar);
            Page.Navigation.PopToRootAsync();
        }
    }
}