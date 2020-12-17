using System;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Patienten.Models;
using medicijn.Utils;
using medicijn.Views.Recepten;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.ViewModels.Patienten
{
    public class PatientDossierViewModel : BaseViewModel
    {
        public ICommand BackButtonPressedCommand { get; }

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

            //Modal.Instance.OpenModal(new DoseringAanpassenView());
        }

        public PatientDossierViewModel(Patient patient) : this()
        {
            Patient = patient;
        }

        public void BackButtonPressed() 
        {
            Navigator.Instance.Pop();
        }
    }
}
