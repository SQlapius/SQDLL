using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace medicijn.Components
{
    public partial class SelectBoxGroup : ContentView
    {
        public static readonly BindableProperty OptionsProperty
            = BindableProperty.Create(
                nameof(Options),
                typeof(IEnumerable),
                typeof(IEnumerable),
                propertyChanged: ok);

        public IEnumerable Options
        {
            get => (IEnumerable)GetValue(OptionsProperty);
            set
            {
                SetValue(OptionsProperty, value);
            }
        }

        public static readonly BindableProperty SelectedIdProperty
            = BindableProperty.Create(
                nameof(SelectedId),
                typeof(int),
                typeof(int),
                propertyChanged: ok);

        public int SelectedId
        {
            get => (int)GetValue(SelectedIdProperty);
            set
            {
                SetValue(SelectedIdProperty, value);
            }
        }

        private static void ok(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine("ok" + newValue);
        }

        public SelectBoxGroup()
        {
            InitializeComponent();
        }
    }
}
