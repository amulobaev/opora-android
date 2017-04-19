using System;

using Opora.Models;

namespace Opora.ViewModels
{
    /// <summary>
    /// Модель представления создания/изменения опоры
    /// </summary>
    public class EditPillarViewModel : BaseViewModel
    {
        public EditPillarViewModel()
        {
            Title = "Опора";
        }

        public EditPillarViewModel(Pillar item) : this()
        {
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
    }
}