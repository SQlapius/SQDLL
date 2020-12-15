using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using XamEffects;

namespace medicijn.Components
{
    public partial class IconButton : ContentView
    {
        public static readonly BindableProperty ButtonText
            = BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(IconButton),
                propertyChanged: TextPropertyChanged);

        public string Text
        {
            get => (string)GetValue(ButtonText);
            set   {
                SetValue(ButtonText, value);
            }
        }

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconButton)bindable).buttonText.Text = newValue as string;
        }

        public static readonly BindableProperty FillProperty
            = BindableProperty.Create(
                nameof(Fill),
                typeof(Color),
                typeof(IconButton),
                propertyChanged: FillPropertyChanged);

        public Color Fill
        {
            get => (Color)GetValue(FillProperty);
            set 
            {
                SetValue(FillProperty, value);
            }
        }

        private static void FillPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if((Color)newValue != Color.White) 
            {
                var myButton = bindable as IconButton;
                ((IconButton)bindable).buttonContainer.BackgroundColor = (Color)newValue;
                ((IconButton)bindable).buttonContainer.BorderColor = (Color)newValue;
                ((IconButton)bindable).buttonIcon.TextColor = Color.White;
                ((IconButton)bindable).buttonText.TextColor = Color.White;
                TouchEffect.SetColor(myButton.ok, Color.White);

            }
        }

        public static readonly BindableProperty IconProperty
          = BindableProperty.Create(
                nameof(Icon),
                typeof(string),
                typeof(IconButton),
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
            ((IconButton)bindable).buttonIcon.Text = newValue as string;
        }

        public static readonly BindableProperty ButtonPaddingProperty
          = BindableProperty.Create(
                nameof(ButtonPadding),
                typeof(Thickness),
                typeof(IconButton),
                propertyChanged: PaddingPropertyChanged 
              );


        public Thickness ButtonPadding
        {
            get => (Thickness)GetValue(IconProperty);
            set
            {
                SetValue(ButtonPaddingProperty, value);
            }
        }

        private static void PaddingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((IconButton)bindable).buttonContainer.Padding = (Thickness)newValue;
        }

        public static readonly BindableProperty ColorProperty
           = BindableProperty.Create(
               nameof(Color),
               typeof(Color),
               typeof(IconButton),
               propertyChanged: ColorPropertyChanged);

        public Color Color
        {
            get => (Color)GetValue(IconProperty);
            set
            {
                SetValue(IconProperty, value);
            }
        }

        private static void ColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var myButton = bindable as IconButton;

            myButton.buttonIcon.TextColor = (Color)newValue;
            myButton.buttonText.TextColor = (Color)newValue;
            myButton.buttonContainer.BorderColor = (Color)newValue;

            TouchEffect.SetColor(myButton.ok, (Color)newValue);
        }

        public static readonly BindableProperty CommandProperty
            = BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(IconButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public IconButton()
        {
            InitializeComponent();
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Command != null)
            {
                Debug.WriteLine("hit");
                Command.Execute(((TappedEventArgs)e).Parameter);
            }
        }
    }
}
