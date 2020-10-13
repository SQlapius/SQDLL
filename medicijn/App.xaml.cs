using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using SQDLL.Patient;
using Newtonsoft.Json;

namespace medicijn
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            var patient = new Patient(119, 13550);
            Debug.WriteLine(JsonConvert.SerializeObject(await patient.GetInfo()));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
