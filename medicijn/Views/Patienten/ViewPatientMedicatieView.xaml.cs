using System;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using medicijn.ViewModels.Patienten;
using Xamarin.Forms;

namespace medicijn.Views.Patienten
{
    public partial class ViewPatientMedicatieView : ContentView
    {
        public ViewPatientMedicatieView()
        {
            InitializeComponent();
        }

        public ViewPatientMedicatieView(Patient patient) : this()
        {
            BindingContext = new ViewPatientMedicatieViewModel(
                patient,
                Navigation
            );
        }
    }
}
