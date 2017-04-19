using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Opora.Views;
using Opora.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Opora
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            Page measurementsPage = new MeasurementsPage();
            measurementsPage.BindingContext = new MeasurementsViewModel(measurementsPage);
            Page pillarsPage = new PillarsPage();
            pillarsPage.BindingContext = new PillarsViewModel(pillarsPage);

            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(measurementsPage)
                    {
                        Title = "Замеры",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new NavigationPage(pillarsPage)
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