using System;
using GZIDAL002.Patienten.Models;

namespace medicijn.ViewModels.Patienten
{
    public class EditPatientCIsViewModel
    {
        public Patient Patient { get; set; }

        public EditPatientCIsViewModel() { }

        public EditPatientCIsViewModel(Patient patient) : this()
        {
            Patient = patient;
        }
    }
}
