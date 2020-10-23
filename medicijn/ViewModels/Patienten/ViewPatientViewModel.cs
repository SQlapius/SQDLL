using System;
using System.Diagnostics;
using Xamarin.Forms;
using GZIDAL002.Patienten.Models;
using System.Threading.Tasks;
using medicijn.Utils;
using System.Windows.Input;
using medicijn.Views.Recepten;
using medicijn.Views.Patienten;
using GZIDAL002.Patienten;
using medicijn.Models;

namespace medicijn.ViewModels.Patienten
{
    public class ViewPatientViewModel : BaseViewModel 
    {
        PatientService _patientSerivce;

        public ICommand CloseOverlayCommand { get; }
        public ICommand ShowPatientMedicationPressedCommand { get; }
        public ICommand CreateNewReceptPressedCommand { get; }
        public ICommand PressedBackButton { get; }
        public ICommand GCButtonPressedCommand { get; }

        public Patient Patient { get; set; }

        public ViewPatientViewModel()
        {
            _patientSerivce = new PatientService();

            CloseOverlayCommand = new Command(CloseOverlay);
            ShowPatientMedicationPressedCommand = new Command(NavigateToPatientMedication);
            CreateNewReceptPressedCommand = new Command(NavigateToCreateNewRecept);
            PressedBackButton = new Command(GoBack);
            GCButtonPressedCommand = new Command(CleanPatientData);
        }

        public ViewPatientViewModel(Patient patient) : this()
        {
            Patient = patient;
        }

        public async void CleanPatientData()
        {
            var success = await _patientSerivce.GCPatient(Patient);

            if (!success)
                Debug.WriteLine("FAILED Garbage Collection");
        }

        public void NavigateToCreateNewRecept()
        {
            Navigator.Instance.Add(
                new NavPage("Nieuw Recept", new MakeReceptView(Patient))
            );
        }

        public void NavigateToPatientMedication()
        {
            Navigator.Instance.Add(
                 new NavPage(
                     "Actieve Medicatie",
                     new ViewPatientMedicatieView(Patient)
                )
            );
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
    