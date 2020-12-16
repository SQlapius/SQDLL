using System;
using System.Collections.Generic;
using medicijn.ViewModels.Main;
using Xamarin.Forms;

namespace medicijn.Views.Main
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation);
        }
    }
}
