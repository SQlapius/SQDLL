using System;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten.Models;
using medicijn.Views.Medicijnen;
using Xamarin.Forms;

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

            Recept = new Recept(patient, new GZIDAL002.Medicijnen.Models.Medicijn(), "test", 5, "20");
        }

        private async void OpenMedicinePicker()
        {
            await _navigation.PushModalAsync(new ZoekMedicijnView(Recept));
        }
    }
}
