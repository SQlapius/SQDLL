using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GZIDAL002.Patienten;
using GZIDAL002.Patienten.Models;
using medicijn.Models;
using medicijn.Utils;
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
        public ICommand ChangeSearchFilterCommand { get; }
        public ICommand ClickedOnFilterItemCommand { get; }

        private string _searchValue;
        public string SearchValue 
        {
            get => _searchValue;
            set
            {
                _searchValue = value;
                OnPropertyChanged();
            }
        }

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

        private IList<DropdownItem> _dropdownItems;
        public IList<DropdownItem> DropdownItems
        {
            get => _dropdownItems;
            set
            {
                _dropdownItems = value;
                OnPropertyChanged();
            }
        }

        private bool _dropdownIsOpen;
        public bool DropdownIsOpen
        {
            get => _dropdownIsOpen;
            set
            {
                _dropdownIsOpen = value;
                OnPropertyChanged();
            }
        }

        private DropdownItem _selectedFilterItem;
        public DropdownItem SelectedFilterItem
        {
            get => _selectedFilterItem;
            set
            {
                _selectedFilterItem = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ZoekPatientViewModel()
        {
            _patientService = new PatientService();

            DropdownItems = new List<DropdownItem>
            {
                new DropdownItem()
                {
                    Naam = "File Nummer",
                    Icon = "\uf1fd",
                    Id = 1
                },
                new DropdownItem()
                {
                    Naam = "Naam",
                    Icon = "\uf007",
                    Id = 2,
                },
                new DropdownItem()
                {
                    Naam = "Sedula",
                    Icon = "\uf2c2",
                    Id = 3
                }
            };

            SelectedFilterItem = DropdownItems[1];

            SearchButtonPressedCommand = new Command(SearchPatient);
            ChangeSearchFilterCommand = new Command(() => DropdownIsOpen = !DropdownIsOpen);
            ClickedOnFilterItemCommand = new Command<int>(ClickedOnFilterItem);
        }

        public ZoekPatientViewModel(INavigation navigation) : this()
        {
            _navigation = navigation;
        }

        private async void SearchPatient()
        {
            IsLoading = true;

            Patients = new ObservableCollection<Patient>(
                await _patientService.ZoekPatient(
                    119,
                    SearchValue,
                    SelectedFilterItem.Id
                )
            );

            IsLoading = false;
        }

        private async void NavigateToPatientView(Patient patient)
        {
            IsLoading = true;

            var pat = await _patientService.GetPatientDetailed(patient.VesId, patient.PatId);
            PatientState.Instance.UpdatePatient(pat);
            await _navigation.PushAsync(new ViewPatientView(pat));

            IsLoading = false;
        }

        private void ClickedOnFilterItem(int id)
        {
            DropdownIsOpen = false;
            SelectedFilterItem = DropdownItems.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
