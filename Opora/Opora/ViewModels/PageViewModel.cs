using Xamarin.Forms;

namespace Opora.ViewModels
{
    public abstract class PageViewModel : BaseViewModel
    {
        private readonly Page _page;

        public PageViewModel(Page page)
        {
            _page = page;
        }

        public Page Page
        {
            get { return _page; }
        }
    }
}