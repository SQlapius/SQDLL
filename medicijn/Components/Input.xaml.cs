using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace medicijn.Components
{
    public partial class Input : ContentView
    {

        public static readonly BindableProperty IconProperty
        = BindableProperty.Create(
            nameof(Input),
            typeof(string),
            typeof(Input),
            propertyChanged: IconPropertyChanged);

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set
            {
                SetValue(IconProperty, value);
            }
        }

        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((Input)bindable).IconContainer.Text = newValue as string;
        }

        public static readonly BindableProperty PlaceholderProperty
        = BindableProperty.Create(
            nameof(Input),
            typeof(string),
            typeof(Input),
            propertyChanged: PlaceholderPropertyChanged);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((Input)bindable).InputBox.Placeholder = newValue as string;
        }

        public static readonly BindableProperty TextProperty
        = BindableProperty.Create(
            nameof(Input),
            typeof(string),
            typeof(Input),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((Input)bindable).InputBox.Text = newValue as string;
        }

        public event EventHandler<EventArgs> Completed;

        public Input()
        {
            InitializeComponent();
        }

        void TappedOnFrame(object sender, EventArgs e)
        {
            InputBox.Focus();
        }

        void InputBox_Completed(System.Object sender, System.EventArgs e)
        {
            Completed.Invoke(this, e);
        }
    }
}
