using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GZIDAL002.Patienten;
using GZIDAL002.Patienten.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace medicijn.ViewModels.Patienten
{
    public class ViewPatientMedicatieViewModel : BaseViewModel
    {
        INavigation _navigation;
        PatientService _patientService;

        public ICommand MakeReceptButtonPressedCommand { get; set; }

        Patient Patient { get; set; }

        public List<int> SelectedMedicatie { get; set; } = new List<int>();

        private ObservableCollection<Medicatie> _medicatie;
        public ObservableCollection<Medicatie> Medicatie
        {
            get => _medicatie;
            set
            {
                _medicatie = value;
                OnPropertyChanged();
            }
        }

        public ViewPatientMedicatieViewModel()
        {
            _patientService = new PatientService();
        }

        public ViewPatientMedicatieViewModel(Patient patient, INavigation navigation) : this()
        {
            _navigation = navigation;
            Patient = patient;

            GetMedicatie();
        }

        private async void GetMedicatie()
        {
            Medicatie = new ObservableCollection<Medicatie>(
                await _patientService.GetPatientMedicatie(Patient)
            );
        }
    }
}
