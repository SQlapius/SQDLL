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
using Plugin.SQ_UIKit;
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

            Navigator.Instance.Add(new Models.NavPage("", new MakeReceptView(patient)));
        }

        async void Overlay_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigator.Instance.Pop();
        }
    }
}
