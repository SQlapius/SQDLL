using System;
using System.Collections.Generic;
using System.Diagnostics;
using GZIDAL002.Recepten.Models;
using medicijn.ViewModels.Recepten;
using Xamarin.Forms;

namespace medicijn.Views.Recepten
{
    public partial class DisplayInfoTextView : ContentView
    {
        public DisplayInfoTextView()
        {
            InitializeComponent();
        }

        public DisplayInfoTextView(ContraIndicatie ci) : this()
        {
            BindingContext = new DisplayInfoTextViewModel(ci);
        }

        public DisplayInfoTextView(Interactie ia) : this()
        {
            BindingContext = new DisplayInfoTextViewModel(ia);
        }
    }
}
