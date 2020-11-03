using System;
using System.Collections.Generic;
using Xamarin.Forms;
using medicijn.ViewModels.Recepten;
using GZIDAL002.Recepten.Models;

namespace medicijn.Views.Recepten
{
    public partial class ConfirmReceptView : ContentView
    {
        public ConfirmReceptView(Recept recept)
        {
            InitializeComponent();

            BindingContext = new ConfirmReceptViewModel(recept);
        }
    }
}
