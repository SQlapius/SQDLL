using System;
using System.Collections.Generic;
using medicijn.ViewModels.Auth;
using Xamarin.Forms;

namespace medicijn.Views.Auth
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
