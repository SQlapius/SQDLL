using System;
using System.Collections.Generic;
using medicijn.ViewModels.Medicijnen;
using Xamarin.Forms;

namespace medicijn.Views.Medicijnen
{
    public partial class ZoekMedicijnView : ContentPage
    {
        public ZoekMedicijnView()
        {
            InitializeComponent();
            BindingContext = new ZoekMedicijnViewModel(Navigation);
        }
    }
}
