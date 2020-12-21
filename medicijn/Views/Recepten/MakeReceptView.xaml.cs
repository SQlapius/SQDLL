using System;
using System.Collections.Generic;
using System.Diagnostics;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten.Models;
using medicijn.ViewModels.Recepten;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace medicijn.Views.Recepten
{
    public partial class MakeReceptView : ContentView
    {
        MakeReceptViewModel _vm;
        public MakeReceptView()
        {
            InitializeComponent();
        }

        public MakeReceptView(Patient patient) : this()
        {
            _vm = new MakeReceptViewModel(Navigation, patient);
            BindingContext = _vm;
        }

        public MakeReceptView(Recept recept) : this()
        {
            _vm = new MakeReceptViewModel(Navigation, recept);
            BindingContext = _vm;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            var currentReceptRegel = sender as VisualElement;
            var id = ((Button)sender).BindingContext;
            var parent = ((Button)sender).Parent as Grid;

            //Reset background color for all children of grid
            foreach(var child in container.Children) 
            {
                child.BackgroundColor = Color.White;
                ((Grid)child).Children[4].BackgroundColor = Color.FromHex("#F8F8F8");
            }

            var currentReceptRegelY = GetScreenCoordinates(currentReceptRegel).Y;
            var IsOutOfView = currentReceptRegelY + Dropdown.Height > scrollview.ContentSize.Height;
            var YCord = !IsOutOfView ? currentReceptRegelY + 30 : 
                GetScreenCoordinates(currentReceptRegel).Y - Dropdown.Height;

            await Dropdown
                .LayoutTo(
                new Rectangle(container.Width - Dropdown.Width - 20, 
                YCord, Dropdown.Width, Dropdown.Height), 0);

            _vm.ClickedReceptRegelMenu((int)id);

            Dropdown.Opacity = _vm.ReceptRegelMenuIsOpen ? 100 : 0;

            //Set highlight color
            parent.BackgroundColor = _vm.ReceptRegelMenuIsOpen ? 
                Color.FromRgba(30, 168, 222, 35) : Color.White;
            parent.Children[4].BackgroundColor = _vm.ReceptRegelMenuIsOpen ? 
                Color.Transparent : Color.FromHex("#F8F8F8");

        }

        public (double X, double Y) GetScreenCoordinates(VisualElement view)
        {
            var parent = view.Parent as VisualElement;
            var screenCoordinateX = parent.X;
            var screenCoordinateY = parent.Y;

            var parentElement = (VisualElement)container;
            while (parent != null && parent.GetType().BaseType == typeof(View))
            {
                screenCoordinateX += parent.X;
                screenCoordinateY += parent.Y;
                parentElement = (VisualElement)parentElement.Parent;
            }
            return (screenCoordinateX, screenCoordinateY);
        }
    }
}
