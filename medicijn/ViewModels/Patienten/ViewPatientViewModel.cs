using System;
using System.Diagnostics;
using Xamarin.Forms;
using GZIDAL002.Patienten.Models;
using System.Threading.Tasks;
using medicijn.Utils;
using System.Windows.Input;
using medicijn.Views.Recepten;
using medicijn.Views.Patienten;

namespace medicijn.ViewModels.Patienten
{
    public class ViewPatientViewModel : BaseViewModel 
    {
        public ICommand CloseOverlayCommand { get; }
        public ICommand ShowPatientMedicationPressedCommand { get; }
        public ICommand CreateNewReceptPressedCommand { get; }
        public ICommand PressedBackButton { get; }

        public Patient Patient { get; set; }

        public ViewPatientViewModel()
        {
            CloseOverlayCommand = new Command(CloseOverlay);
            ShowPatientMedicationPressedCommand = new Command(NavigateToPatientMedication);
            CreateNewReceptPressedCommand = new Command(NavigateToCreateNewRecept);
            PressedBackButton = new Command(GoBack);
        }

        public ViewPatientViewModel(Patient patient) : this()
        {
            Patient = patient;
        }

        public void NavigateToCreateNewRecept()
        {
            Navigator.Instance.Add(new MakeReceptView(Patient));
        }

        public void NavigateToPatientMedication()
        {
            Navigator.Instance.Add(new ViewPatientMedicatieView(Patient));
        }

        public void CloseOverlay()
        { 
            Modal.Instance.IsVisible = false;
        }

        public void GoBack()
        {
            Navigator.Instance.Pop();
        }
    }
}
    