using System;
using System.Diagnostics;
using GZIDAL002.Patienten.Models;
using Newtonsoft.Json;

namespace medicijn.ViewModels.Patienten
{
    public class PatientDossierViewModel
    {
        public PatientDossierViewModel()
        {
        }

        public PatientDossierViewModel(Patient patient) : this()
        {
            Debug.WriteLine(JsonConvert.SerializeObject(patient));
        }
    }
}