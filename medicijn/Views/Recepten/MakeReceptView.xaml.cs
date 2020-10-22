using System;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using medicijn.ViewModels.Recepten;
using Xamarin.Forms;

namespace medicijn.Views.Recepten
{
    public partial class MakeReceptView : ContentView
    {
        public MakeReceptView()
        {
            InitializeComponent();
        }

        public MakeReceptView(Patient patient) : this()
        {
            BindingContext = new MakeReceptViewModel(Navigation, patient);
        }
    }
}
