using System;
using System.Collections.Generic;
using medicijn.ViewModels.Patienten;
using Xamarin.Forms;

namespace medicijn.Views.Patienten
{
    public partial class ZoekPatientView : ContentPage
    {
        public ZoekPatientView()
        {
            InitializeComponent();

            BindingContext = new ZoekPatientViewModel(Navigation);
        }
    }
}
