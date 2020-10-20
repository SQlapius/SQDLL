using System;
using System.Diagnostics;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using medicijn.ViewModels.Patienten;
using medicijn.Views.Recepten;
using Newtonsoft.Json;
using System.ComponentModel;
using Xamarin.Forms;
using medicijn.Utils;

namespace medicijn.Views.Patienten
{
    public partial class ViewPatientView : ContentPage
    {
        private Modal _vm;
        public ViewPatientView()
        {
            InitializeComponent();
            test.BackgroundColor = Color.FromRgba(0, 0, 0, 0.5);
        }
        public ViewPatientView(Patient patient) : this()
        {
            BindingContext = new ViewPatientViewModel(patient);
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ok.Content = new MakeReceptView(Navigation, ((ViewPatientViewModel)BindingContext).Patient);
        }

        async void test_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsVisible") {
                if(Modal.Instance.IsVisible)
                {
                    await test.FadeTo(1, 500, Easing.SpringOut);
                } else {
                    
                    await test.FadeTo(0, 500, Easing.SpringOut);
                }
            }
            return;
        }

        void Button_Clicked_1(object sender, System.EventArgs e)
        {
            ok.Content = new ViewPatientMedicatieView(((ViewPatientViewModel)BindingContext).Patient);
        }
    }
}
