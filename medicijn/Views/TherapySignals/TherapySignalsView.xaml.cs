using System;
using System.Collections.Generic;
using Xamarin.Forms;
using medicijn.ViewModels.TherapySignals;

namespace medicijn.Views.TherapySignals
{
    public partial class TherapySignalsView : ContentView
    {
        public TherapySignalsView()
        {
            InitializeComponent();
            BindingContext = new TherapySignalsViewModel();
        }
    }
}
