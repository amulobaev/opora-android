using GalaSoft.MvvmLight.Command;
using Opora.Models;
using System.Windows.Input;

namespace Opora.ViewModels
{
    public abstract class EditorViewModel<T> : PageViewModel where T : BaseModel
    {
        private ICommand _saveCommand;

        public T Item { get; set; }

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        protected abstract void Save();
    }
}