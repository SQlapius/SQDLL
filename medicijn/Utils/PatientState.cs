using System;
using GZIDAL002.Patienten.Models;

namespace medicijn.Utils
{
    public class PatientState : BaseViewModel
    {
        public static PatientState Instance { get; private set; }

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

        private PatientState() 
        {
        }

        static PatientState() 
        {
            Instance = new PatientState();
        }

        public void UpdatePatient(Patient patient) 
        {
            Patient = patient;
        }
    }
}
