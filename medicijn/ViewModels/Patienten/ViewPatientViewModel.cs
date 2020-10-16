using System;
using System.Diagnostics;
using GZIDAL002.Patienten.Models;

namespace medicijn.ViewModels.Patienten
{
    public class ViewPatientViewModel
    {
        public Patient Patient { get; set; }

        public ViewPatientViewModel() { }

        public ViewPatientViewModel(Patient patient)
        {
            Patient = patient;
        }
    }
}
