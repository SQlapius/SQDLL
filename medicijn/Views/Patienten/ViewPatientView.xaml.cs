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
            BindingContext = new ViewPatientViewModel(patient, Navigation);

            Navigator.Instance.Add(new Models.NavPage("", new MakeReceptView(patient)));

            Modal.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        async private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsVisible") {
                if(Modal.Instance.IsVisible)
                {
                    Overlay.IsVisible = true;
                    await Overlay.FadeTo(1, 500, Easing.SpringOut);
                } 
                else 
                {
                    Overlay.IsVisible = false;
                    await Overlay.FadeTo(0, 500, Easing.SpringOut);
                }
            }
        }

        void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            Navigator.Instance.Pop();
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Modal.Instance.CloseModal();
        }

        async void Button_Pressed(System.Object sender, System.EventArgs e)
        {
            if(!MenuBar.IsVisible)
            {
                MenuBar.IsVisible = true;
                await MenuBar.TranslateTo(0, -40, 200, Easing.SinIn);
            }
            else
            { 
                await MenuBar.TranslateTo(0, 70, 250, Easing.SinOut);
                await Task.Delay(300);
                MenuBar.IsVisible = false;
            }
        }
    }
}
