using System;
using System.Windows.Input;
using GZIDAL002.Patienten;
using Xamarin.Forms;

namespace medicijn.ViewModels.Patienten
{
    public class ZoekPatientViewModel : BaseViewModel
    {
        PatientService _patientService;

        public ICommand SearchButtonPressedCommand { get; }

        public string SearchValue { get; set; }

        public ZoekPatientViewModel()
        {
            _patientService = new PatientService();

            SearchButtonPressedCommand = new Command(SearchPatient);
        }

        public void SearchPatient()
        {
            _patientService.ZoekPatient(119, SearchValue);
        }
    }
}
