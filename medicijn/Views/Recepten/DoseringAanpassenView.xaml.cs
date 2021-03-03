using System;
using System.Collections.Generic;
using GZIDAL002.Recepten.Models;
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

        public DoseringAanpassenView(int id, Action<ReceptRegel> updateDosering) : this()
        {
            BindingContext = new DoseringAanpassenViewModel(id, updateDosering);
        }
    }
}
