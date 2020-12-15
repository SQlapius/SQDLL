using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Newtonsoft.Json;
using medicijn.Views.Patienten;
using medicijn.Views.Medicijnen;
using medicijn.Views.Recepten;
using medicijn.Utils;
using medicijn.Views.Auth;

[assembly: ExportFont("FontAwesome5Pro-Light.otf", Alias = "FaLight")]
[assembly: ExportFont("FontAwesome5Pro-Regular.otf", Alias = "FaRegular")]
[assembly: ExportFont("FontAwesome5Pro-Solid.otf", Alias = "FaSolid")]
[assembly: ExportFont("FontAwesome5Brands-Regular.otf", Alias = "FaBrands")]

namespace medicijn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginView());
        }

        protected override async void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}







