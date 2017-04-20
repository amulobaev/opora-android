using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Opora.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Opora
{
    public partial class App : Application
    {
        private static ViewModelLocator _locator;

        /// <summary>
        /// Конструктор
        /// </summary>
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static ViewModelLocator Locator
        {
            get { return _locator ?? (_locator = new ViewModelLocator()); }
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new MeasurementsPage())
                    {
                        Title = "Замеры",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new NavigationPage(new PillarsPage())
                    {
                        Title = "Опоры",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "О программе",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                }
            };
        }
    }
}