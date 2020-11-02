using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GZIDAL002.Patienten;
using GZIDAL002.Recepten;
using GZIDAL002.Recepten.Models;
using GZIDAL002.Patienten.Models;
using Xamarin.Forms;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Input;
using Medicatie = medicijn.Models.Medicatie;
using medicijn.Utils;

namespace medicijn.ViewModels.Patienten
{
    public class ViewPatientMedicatieViewModel : BaseViewModel
    {
        INavigation _navigation;
        PatientService _patientService;
        ReceptService _receptService;

        public ICommand MakeReceptButtonPressedCommand { get; set; }
        public ICommand ClickedOnMedicijnCommand { get; set; }
        public ICommand CancelPressedCommand { get; set; }

        Patient Patient { get; set; }

        public List<int> SelectedMedicatie { get; set; } = new List<int>();

        private int _selectedMedicatiesCount;
        public int SelectedMedicatiesCount
        {
            get => _selectedMedicatiesCount;
            set
            {
                _selectedMedicatiesCount = value;
                OnPropertyChanged();
            }
        }

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
            _receptService = new ReceptService();

            MakeReceptButtonPressedCommand = new Command(MakeReceptPressed);
            ClickedOnMedicijnCommand = new Command<int>(ClickedOnMedicijn);
            CancelPressedCommand = new Command(CancelPressed);
        }

        public ViewPatientMedicatieViewModel(Patient patient, INavigation navigation) : this()
        {
            _navigation = navigation;
            Patient = patient;

            GetMedicatie();
        }

        private async void GetMedicatie()
        {
            var med = await _patientService.GetPatientMedicatie(Patient);
            var medicaties = med
                  .Select(x => new Medicatie(x))
                  .ToList();

            Medicatie = new ObservableCollection<Medicatie>(
                medicaties
            );

        }

        private void ClickedOnMedicijn(int medId)
        {
            var clickedMedicatie = Medicatie.Where(x => x.MedId == medId).FirstOrDefault();
            int index = Medicatie.IndexOf(clickedMedicatie);
            clickedMedicatie.IsChecked = !clickedMedicatie.IsChecked;
            Medicatie[index] = clickedMedicatie;

            SelectedMedicatiesCount = Medicatie
                                        .Where(x => x.IsChecked == true)
                                        .Select(x => x.MedId)
                                        .ToList().Count();
        }

        private async void MakeReceptPressed()
        {
            try
            { 
                var medicaties = Medicatie
                                .Where(x => x.IsChecked == true)
                                .Select(x => x.MedId)
                                .ToList();

                var recept = new Recept(Patient, "Londy");

                var response = await _receptService.AddBestaandeMedicatieToRecept(recept, medicaties);

                Debug.WriteLine(JsonConvert.SerializeObject(response));
            
            }
            catch(Exception e)
            {
                Debug.WriteLine(JsonConvert.SerializeObject(e));
            }
        }

        private void CancelPressed()
        {
            Navigator.Instance.Pop();
        }
    }
}
