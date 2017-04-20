using System;

using Opora.Models;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Opora.ViewModels
{
    /// <summary>
    /// Модель представления создания/изменения опоры
    /// </summary>
    public class EditPillarViewModel : PageViewModel
    {
        public EditPillarViewModel(Pillar item)
        {
            Title = "Опора";
            Name = item.Name;
            Height = item.Height.ToString();
            Taper = item.Taper.ToString();
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        private string _height;

        public string Height
        {
            get { return _height; }
            set { Set(() => Height, ref _height, value); }
        }

        private string _taper;

        public string Taper
        {
            get { return _taper; }
            set { Set(() => Taper, ref _taper, value); }
        }

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        private void Save()
        {
            Pillar pillar = new Pillar { Name = Name };
            MessagingCenter.Send(this, "AddItem", pillar);
            Page.Navigation.PopToRootAsync();
        }
    }
}