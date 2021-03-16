using System;
using System.Collections.Generic;
using System.Diagnostics;
using GZIDAL002.Patienten.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.Components
{
    public partial class ModalHeader : ContentView
    {
        public static readonly BindableProperty PatientProperty
        = BindableProperty.Create(
        nameof(Patient),
        typeof(Patient),
        typeof(ModalHeader),
        propertyChanged: PatientChanged);

        public Patient Patient
        {
            get => (Patient)GetValue(PatientProperty);
            set
            {
                SetValue(PatientProperty, value);
            }
        }

        private static void PatientChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public static readonly BindableProperty TitleProperty
        = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ModalHeader),
        propertyChanged: TitleChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public ModalHeader()
        {
            InitializeComponent();
        }
    }
}
