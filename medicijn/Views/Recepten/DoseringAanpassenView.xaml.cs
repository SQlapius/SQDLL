using System;
using System.Collections.Generic;
using medicijn.ViewModels.Recepten;
using Xamarin.Forms;

namespace medicijn.Views.Recepten
{
    public partial class DoseringAanpassenView : ContentView
    {
        public DoseringAanpassenView()
        {
            InitializeComponent();

            BindingContext = new DoseringAanpassenViewModel();
        }

        public DoseringAanpassenView(int id) : this()
        {
            BindingContext = new DoseringAanpassenViewModel(id);
        }
    }
}
