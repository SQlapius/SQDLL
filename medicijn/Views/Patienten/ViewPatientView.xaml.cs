using System;
using System.Diagnostics;
using System.Collections.Generic;
using GZIDAL002.Patienten.Models;
using medicijn.ViewModels.Patienten;
using medicijn.Views.Recepten;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using Xamarin.Forms;
using medicijn.Utils;
using medicijn.Views.Medicijnen;

namespace medicijn.Views.Patienten
{
    public partial class ViewPatientView : ContentPage
    {
        Modal _vm;

        public ViewPatientView()
        {
            InitializeComponent();
            Overlay.BackgroundColor = Color.FromRgba(0, 0, 0, 0.5);
            Navigator.Instance.SetMainPage(ok);
        }

        public ViewPatientView(Patient patient) : this()
        {
            BindingContext = new ViewPatientViewModel(patient);
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigator.Instance.Add(new MakeReceptView(Navigation, ((ViewPatientViewModel)BindingContext).Patient));
        }

        async void Overlay_PropertyChanged(System.Object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsVisible") {
                if(Modal.Instance.IsVisible)
                {
                    await Overlay.FadeTo(1, 500, Easing.SpringOut);
                } 
                else 
                {
                    await Overlay.FadeTo(0, 500, Easing.SpringOut);
                }
            }
        }

        void Button_Clicked_1(object sender, System.EventArgs e)
        {
            ok.Content = new ViewPatientMedicatieView(((ViewPatientViewModel)BindingContext).Patient);
        }
    }
}
