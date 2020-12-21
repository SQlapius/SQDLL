using System;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using Xamarin.Forms;

namespace medicijn.ViewModels.Patienten
{
    public partial class EditPatientCIsView : ContentView
    {
        public EditPatientCIsView()
        {
            InitializeComponent();
        }

        public EditPatientCIsView(Patient patient) : this()
        {
            BindingContext = new EditPatientCIsViewModel(patient);
        }
    }
}
