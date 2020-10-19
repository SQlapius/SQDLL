using System;
using System.Collections.Generic;
using GZIDAL002.Recepten.Models;
using medicijn.ViewModels.Medicijnen;
using Xamarin.Forms;

namespace medicijn.Views.Medicijnen
{
    public partial class ZoekMedicijnView : ContentView
    {
        public ZoekMedicijnView()
        {
            InitializeComponent();

            //BindingContext = new ZoekMedicijnViewModel(Navigation);
        }

        public ZoekMedicijnView(Recept recept) : this()
        {
            BindingContext = new ZoekMedicijnViewModel(Navigation, recept);
        }
    }
}
