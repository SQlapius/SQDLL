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

            BackButton.Children.Insert(0, new IconLabel(IconLabel.Icon.Chevron_Left, IconLabel.Type.Solid)
            {
                FontSize = 20,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            });
        }

        public ViewPatientView(Patient patient) : this()
        {
            BindingContext = new ViewPatientViewModel(patient);
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
    }
}
