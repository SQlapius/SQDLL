using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Patienten;
using GZIDAL002.Patienten.Models;
using medicijn.Views.Patienten;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.ViewModels.Patienten
{
    public class ZoekPatientViewModel : BaseViewModel
    {
        INavigation _navigation;
        PatientService _patientService;

        public ICommand SearchButtonPressedCommand { get; }

        public string SearchValue { get; set; }

        public Patient SelectedPatient
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                NavigateToPatientView(value);
            }
        }

        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                OnPropertyChanged();
            }
        }

        public ZoekPatientViewModel()
        {
            _patientService = new PatientService();

            SearchButtonPressedCommand = new Command(SearchPatient);
        }

        public ZoekPatientViewModel(INavigation navigation) : this()
        {
            _navigation = navigation;
        }

        private async void SearchPatient()
        {
            Patients = new ObservableCollection<Patient>(
                await _patientService.ZoekPatient(119, SearchValue)
            );

            Debug.WriteLine(JsonConvert.SerializeObject(Patients));
        }

        private async void NavigateToPatientView(Patient patient)
        {
            await _navigation.PushAsync(new ViewPatientView(patient));
        }
    }
}
