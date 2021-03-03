using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten;
using GZIDAL002.Recepten.Models;
using medicijn.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.ViewModels.Patienten
{
    public enum EditPatientCIsState
    {
        showActiveCIs,
        showAllCIs,
        showSpecificCIInfo
    }

    public class EditPatientCIsViewModel : BaseViewModel
    {
        IKeyboardHelper _keyboard;
        ReceptService _receptService;

        public ICommand SearchButtonPressedCommand { get; }
        public ICommand DetailedCIStateBackButtonPressedCommand { get; }
        public ICommand AddCIButtonPressedCommand { get; }
        public ICommand DeleteCIButtonPressedCommand { get; }
        public ICommand DeleteInfoStateCIButtonPressedCommand { get; }
        public ICommand ShowActiveCIsButtonPressedCommand { get; }

        public Patient Patient { get; set; }

        private LOV _selectedCI;
        public LOV SelectedCI
        {
            get => _selectedCI;
            set
            {
                if (value == null)
                    return;

                _selectedCI = value;

                HandleCIPressed(value);
                OnPropertyChanged();
            }
        }

        public CIPatient SelectedActiveCI
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                HandleCIPressed(value);
                OnPropertyChanged();
            }
        }

        private WebView _webView;
        public WebView WebView
        {
            get => _webView;
            set
            {
                _webView = value;
                OnPropertyChanged();
            }
        }

        private string _searchValue;
        public string SearchValue
        {
            get => _searchValue;
            set
            {
                _searchValue = value;
                _selectedCI = null;

                HandleSearchValueChanged(value);
                OnPropertyChanged();
            }
        }

        private EditPatientCIsState _state;
        public EditPatientCIsState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CIPatient> _activeCIs;
        public ObservableCollection<CIPatient> ActiveCIs
        {
            get => _activeCIs;
            set
            {
                _activeCIs = value;
                OnPropertyChanged();
            }
        }

        private List<LOV> _cis;
        public List<LOV> CIs
        {
            get => _cis;
            set
            {
                _cis = value;
                OnPropertyChanged();
            }
        }

        private List<LOV> _filteredCIs;
        public List<LOV> FilteredCIs
        {
            get => _filteredCIs;
            set
            {
                _filteredCIs = value;
                OnPropertyChanged();
            }
        }

        public EditPatientCIsViewModel()
        {
            _keyboard = DependencyService.Get<IKeyboardHelper>();
            _receptService = new ReceptService();

            SearchButtonPressedCommand = new Command(() => _keyboard.HideKeyboard());
            DeleteCIButtonPressedCommand = new Command<CIPatient>(DeletePatientCI);
            DeleteInfoStateCIButtonPressedCommand = new Command<LOV>(DeletePatientCI);
            AddCIButtonPressedCommand = new Command<LOV>(AddCIToPatient);
            ShowActiveCIsButtonPressedCommand = new Command(ShowActiveCIs);
            DetailedCIStateBackButtonPressedCommand = new Command(() => {
                State = SearchValue == "" ? EditPatientCIsState.showActiveCIs :
                    EditPatientCIsState.showAllCIs;
            });

            State = EditPatientCIsState.showActiveCIs;

            GetAllCIs();
        }

        public EditPatientCIsViewModel(Patient patient) : this()
        {
            Patient = patient;

            ActiveCIs = new ObservableCollection<CIPatient>(
                patient.ContraIndicaties ?? new List<CIPatient>()
            );
        }

        private async Task<bool> ConfirmAddCIPatient(LOV ci)
        {
            return await Application.Current.MainPage.DisplayAlert(
                "Are you sure?",
                $"Wil je {ci.Naam} toevoegen bij de patient {Patient.Naam}#{Patient.Sedula}",
                "yes",
                "no"
            );
        }

        private async void AddCIToPatient(LOV ci)
        {
            if (!await ConfirmAddCIPatient(ci))
                return;

            if (!IsCIAlreadyRegistered(ci))
            {
                ActiveCIs.Insert(0, new CIPatient()
                {
                    MedNaam = ci.Naam,
                    CICode = ci.Id,
                    ToevoegDatum = DateTime.Now,
                    Arts = new Arts()
                    {
                        Naam = "Zjerlondy Ferero",
                        Unico = 1111
                    }
                });
            }

            SearchValue = "";
            State = EditPatientCIsState.showActiveCIs;
        }

        private async Task<bool> ConfirmDeleteCIPatient(CIPatient ci)
        {
            return await Application.Current.MainPage.DisplayAlert(
                "Are you sure?",
                $"Wil je {ci.MedNaam} verwijderen van de patient {Patient.Naam}#{Patient.Sedula}",
                "yes",
                "no"
            );
        }

        private bool IsCIAlreadyRegistered(LOV ci)
        {
            return ActiveCIs.Where(x => x.CICode == ci.Id).ToList().Count > 0;
        }

        private void DeletePatientCI(LOV ci)
        {
            var patCI = ActiveCIs.FirstOrDefault(x => x.CICode == ci.Id);

            if (patCI != null)
            {
                DeletePatientCI(patCI);
            }
        }

        private async void DeletePatientCI(CIPatient ci)
        {
            if(await ConfirmDeleteCIPatient(ci))
            {
                ActiveCIs.Remove(ci);

                State = EditPatientCIsState.showActiveCIs;
                SearchValue = "";
            }
        }

        private void HandleSearchValueChanged(string value)
        {
            if (value.Trim() == "")
            {
                State = EditPatientCIsState.showActiveCIs;
                FilteredCIs = new List<LOV>();
            }
            else
            {
                State = EditPatientCIsState.showAllCIs;
                FilteredCIs = CIs.Where(CIContainsSearchValue).ToList();
            }
        }

        private bool CIContainsSearchValue(LOV x)
        {
            return x.Naam.ToLower().Contains(SearchValue.Trim().ToLower());
        }

        private async void HandleCIPressed(LOV ci)
        {
            var ciInfoText = await _receptService.GetCIInfoTekst(ci.Id);

            DisplayCIInfoInWebView(ciInfoText);
        }

        private async void HandleCIPressed(CIPatient ci)
        {
            SelectedCI = new LOV()
            {
                Id = ci.CICode,
                Naam = ci.MedNaam,
            };

            var ciInfoText = await _receptService.GetCIInfoTekst(SelectedCI.Id);

            DisplayCIInfoInWebView(ciInfoText);
        }
        private void ShowActiveCIs()
        {
            _keyboard.HideKeyboard();

            SearchValue = "";
            State = EditPatientCIsState.showActiveCIs;
        }

        private void DisplayCIInfoInWebView(string ciInfoText)
        {
            _keyboard.HideKeyboard();

            var webView = new WebView();
            var webViewSource = new HtmlWebViewSource
            {
                Html = ciInfoText
            };

            webView.VerticalOptions = LayoutOptions.FillAndExpand;
            webView.HorizontalOptions = LayoutOptions.FillAndExpand;
            webView.Source = webViewSource;
            //webView.Navigating += WebView_Navigating;

            WebView = webView;
            State = EditPatientCIsState.showSpecificCIInfo;
        }

        private async void GetAllCIs()
        {
            CIs = await _receptService.GetAllCI(); 
        }
    }
}
