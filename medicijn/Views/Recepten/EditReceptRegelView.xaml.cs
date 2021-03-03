using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GZIDAL002.Recepten.Models;
using medicijn.ViewModels.Recepten;

namespace medicijn.Views.Recepten
{
    public partial class EditReceptRegelView : ContentView
    {
        public EditReceptRegelView(ReceptRegel receptRegel, Action<ReceptRegel> EditReceptRegel)
        {
            InitializeComponent();

            BindingContext = new EditReceptRegelViewModel(receptRegel, EditReceptRegel);
        }
    }
}