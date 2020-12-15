using System;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using medicijn.ViewModels.Patienten;
using Xamarin.Forms;

namespace medicijn.Views.Patienten
{
    public partial class PatientDossierView : ContentView
    {
        public PatientDossierView()
        {
            InitializeComponent();
        }

        public PatientDossierView(Patient patient) : this()
        {
            BindingContext = new PatientDossierViewModel(patient);
        }
    }
}
