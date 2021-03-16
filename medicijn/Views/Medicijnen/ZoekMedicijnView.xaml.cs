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

        void Entry_Completed(System.Object sender, System.EventArgs e)
        {
            if(_vm != null)
            {
                _vm.SearchMedicijn();
            }
        }
    }
}
