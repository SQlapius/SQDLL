using System;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten.Models;
using medicijn.Views.Medicijnen;
using Xamarin.Forms;
using medicijn.Utils;

namespace medicijn.ViewModels.Recepten
{
    public class MakeReceptViewModel : BaseViewModel
    {
        INavigation _navigation;

        public ICommand NewReceptLinePressedCommand { get; set; }

        public Recept Recept { get; set; }

        public Patient Patient { get; set; }

        public MakeReceptViewModel()
        {
            NewReceptLinePressedCommand = new Command(OpenMedicinePicker);
        }

        public  MakeReceptViewModel(INavigation navigation, Patient patient) : this()
        {
            _navigation = navigation;

            Recept = new Recept(patient);
        }

        private async void OpenMedicinePicker()
        {
            //await _navigation.PushModalAsync(new ZoekMedicijnView(Recept));
            Modal.Instance.IsVisible = true;
            Modal.Instance.Content = new ZoekMedicijnView(Recept);
        }
    }
}
