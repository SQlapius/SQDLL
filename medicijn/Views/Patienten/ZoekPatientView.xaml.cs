using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using medicijn.ViewModels.Patienten;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.Views.Patienten
{
    public partial class ZoekPatientView : ContentPage
    {
        ZoekPatientViewModel _vm;
        public ZoekPatientView()
        {
            InitializeComponent();

            _vm = new ZoekPatientViewModel(Navigation);

            BindingContext = _vm;

            _vm.PropertyChanged += vm_propertyChanged;
        }

        private void vm_propertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DropdownIsOpen")
            {
                if (_vm.DropdownIsOpen)
                {
                    var heightAnimation = new Animation(x => Dropdown.HeightRequest = x, 0, 120);
                    heightAnimation.Commit(Dropdown, "HeightAnimation", 10, 150, Easing.Linear);
                    Cheveron.RotateTo(180, 100);
                    Children.FadeTo(1, 300);
                    Children.IsVisible = true;
                    Children.Opacity = 1;
                }
                else
                {
                    Cheveron.RotateTo(0, 100);
                    Children.IsVisible = false;
                    Children.Opacity = 0;
                }
            }
        }
    }
}
