using System;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using medicijn.ViewModels.Patienten;
using Xamarin.Forms;

namespace medicijn.Views.Patienten
{
    public partial class ViewPatientView : ContentPage
    {
        public ViewPatientView()
        {
            InitializeComponent();

            var patient = new Patient();

            BindingContext = new ViewPatientViewModel(patient);
           
        }
    }
}
