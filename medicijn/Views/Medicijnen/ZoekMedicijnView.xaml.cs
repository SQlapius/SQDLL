using System;
using System.Diagnostics;
using System.Collections.Generic;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Recepten.Models;
using medicijn.ViewModels.Medicijnen;
using Plugin.SQ_UIKit;
using Xamarin.Forms;

namespace medicijn.Views.Medicijnen
{
    public partial class ZoekMedicijnView : ContentView
    {
        ZoekMedicijnViewModel _vm;

        public ZoekMedicijnView()
        {
            InitializeComponent();

            CloseIcon.Children.Insert(0, new IconLabel(IconLabel.Icon.Times, IconLabel.Type.Light)
            {
                FontSize = 16,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center
            });
        }

        public ZoekMedicijnView(Recept recept) : this()
        {

            BindingContext = new ZoekMedicijnViewModel(Navigation, recept);
        }

        public ZoekMedicijnView(Action<Medicijn, int, string> addRegelToRecept) : this()
        {
            _vm = new ZoekMedicijnViewModel(Navigation, addRegelToRecept);
            BindingContext = _vm;
        }

        void entry_Completed(System.Object sender, System.EventArgs e)
        {
            if(_vm != null)
            {
                _vm.SearchMedicijn();
            }
        }
    }
}
