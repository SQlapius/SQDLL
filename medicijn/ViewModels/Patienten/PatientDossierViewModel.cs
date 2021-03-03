using System;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten;
using medicijn.Utils;
using medicijn.Views.Recepten;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.ViewModels.Patienten
{
    public class PatientDossierViewModel : BaseViewModel
    {
        ReceptService _receptService;

        public ICommand BackButtonPressedCommand { get; }
        public ICommand EditCIsButtonPressedCommand { get; }

        private Patient _patient;
        public Patient Patient 
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged();
            }
        }
        
        public PatientDossierViewModel()
        {
            BackButtonPressedCommand = new Command(BackButtonPressed);
            EditCIsButtonPressedCommand = new Command(OpenEditCIsModal);

            _receptService = new ReceptService();

            Modal.Instance.OpenModal(new DoseringAanpassenView());
        }

        public PatientDossierViewModel(Patient patient) : this()
        {
            Patient = patient;
        }

        private void OpenEditCIsModal()
        {
            Modal.Instance.OpenModal(new EditPatientCIsView(Patient));
        }

        private void BackButtonPressed() 
        {
            Navigator.Instance.Pop();
        }
    }
}
