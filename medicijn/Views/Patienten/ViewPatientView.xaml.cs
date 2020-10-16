using System;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using medicijn.ViewModels.Patienten;
using medicijn.Views.Recepten;
using Xamarin.Forms;

namespace medicijn.Views.Patienten
{
    public partial class ViewPatientView : ContentPage
    {
        public ViewPatientView()
        {
            InitializeComponent();
        }

        public ViewPatientView(Patient patient) : this()
        {
            BindingContext = new ViewPatientViewModel(patient);
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ok.Content = new MakeReceptView(Navigation, ((ViewPatientViewModel)BindingContext).Patient);
        }
    }
}
