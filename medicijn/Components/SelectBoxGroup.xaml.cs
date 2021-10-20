using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
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
                propertyChanged: OptionsChanged);

        public IEnumerable Options
        {
            get => (IEnumerable)GetValue(OptionsProperty);
            set
            {
                SetValue(OptionsProperty, value);
            }
        }

        private static void OptionsChanged(BindableObject bindable, object oldValue, object newValue)
        {
        }

        public static readonly BindableProperty SelectedIdProperty
            = BindableProperty.Create(
                nameof(SelectedId),
                typeof(int),
                typeof(int),
                defaultValue: -1,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: IdChanged);

        public int SelectedId
        {
            get => (int)GetValue(SelectedIdProperty);
            set
            {
                SetValue(SelectedIdProperty, value);
            }
        }

        private static void IdChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (SelectBoxGroup)bindable;

            foreach (var c in view.Container.Children)
            {
                var child = (Frame)c;
                child.BackgroundColor = Color.FromHex("#F8F8F8");
                ((Label)child.Content).TextColor = Color.FromHex("#B2B2B2");
            }

            var frame = (Frame)view.Container.Children[(int)newValue];

            frame.BackgroundColor = Color.FromRgba(1, 153, 153, 60);
            ((Label)frame.Content).TextColor = Color.FromRgb(1, 153, 153);
        }

        public SelectBoxGroup()
        {
            InitializeComponent();
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var id = ((TappedEventArgs)e).Parameter;

            SelectedId = (int)id;
        }
    }
}
