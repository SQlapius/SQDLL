using System;
using System.Collections.Generic;
using System.Diagnostics;
using GZIDAL002.Recepten.Models;
using medicijn.ViewModels.Recepten;
using Xamarin.Forms;

namespace medicijn.Views.Recepten
{
    public partial class CIInfoTextView : ContentView
    {
        public CIInfoTextView()
        {
            InitializeComponent();
        }

        public CIInfoTextView(ContraIndicatie ci) : this()
        {
            BindingContext = new CIInfoTextViewModel(ci);
        }
    }
}
