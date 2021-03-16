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
        INavigation _navigation;

        PatientService _patientSerivce;

        public ICommand CloseOverlayCommand { get; }
        public ICommand ShowPatientMedicationPressedCommand { get; }
        public ICommand CreateNewReceptPressedCommand { get; }
        public ICommand PressedBackButton { get; }
        public ICommand GCButtonPressedCommand { get; }
        public ICommand ViewPatientDossierCmmmand { get; }
        public ICommand BackButtonPressedCommand { get; }

        public Patient Patient { get; set; }

        public ViewPatientViewModel()
        {
            _patientSerivce = new PatientService();

            CloseOverlayCommand = new Command(CloseOverlay);
            ShowPatientMedicationPressedCommand = new Command(NavigateToPatientMedication);
            CreateNewReceptPressedCommand = new Command(NavigateToCreateNewRecept);
            PressedBackButton = new Command(GoBack);
            GCButtonPressedCommand = new Command(CleanPatientData);
            ViewPatientDossierCmmmand = new Command(NavigateToViewPatientDossier);
            BackButtonPressedCommand = new Command(BackButtonPressed);
        }

        public ViewPatientViewModel(Patient patient, INavigation navigation) : this()
        {
            Patient = patient;

            _navigation = navigation;
        }

        public async void CleanPatientData()
        {
            var success = await _patientSerivce.GCPatient(Patient, true);

            if (!success)
                Debug.WriteLine("FAILED Garbage Collection");
        }

        private async void BackButtonPressed()
        {
            await _navigation.PopAsync();
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

        public void NavigateToViewPatientDossier()
        {
            Navigator.Instance.Add(
                 new NavPage(
                     "View Patient Dossier",
                     new PatientDossierView(Patient)
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
    