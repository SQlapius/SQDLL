using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Medicijnen;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Recepten.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using medicijn.Utils;

namespace medicijn.ViewModels.Medicijnen
{
    public class ZoekMedicijnViewModel : BaseViewModel
    {
        INavigation _navigation;
        MedicijnService _medicijnService;

        private Action<Medicijn, int, string> _addRegelToRecept;

        public ICommand SearchButtonPressedCommand { get; }
        public ICommand CloseOverlayCommand { get; }

        public string SearchValue { get; set; }

        public string Aantal { get; set; }

        public string Dosering { get; set; }

        public Recept Recept { get; set; }

        public Medicijn SelectedMedicijn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                AddMedicijnToRecept(value);
            }
        }

        private ObservableCollection<Medicijn> _medicijnen;
        public ObservableCollection<Medicijn> Medicijnen
        {
            get
            {
                if (SearchValue == "" || SearchValue == null) {
                    HeaderText = "RECENT SEARCHES";
                    return _recentSearches;
                }
                return _medicijnen; 
            }
            set
            {
                _medicijnen = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Medicijn> _recentSearches;
        public ObservableCollection<Medicijn> RecentSearches
        {
            get => _recentSearches;
            set
            {
                _recentSearches = value;
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

        private string _headerText;
        public string HeaderText
        {
            get => _headerText;
            set
            {
                _headerText = value;
                OnPropertyChanged();
            }
        }

        public ZoekMedicijnViewModel()
        {
            _medicijnService = new MedicijnService();

            SearchButtonPressedCommand = new Command(SearchMedicijn);
            CloseOverlayCommand = new Command(CloseOverlay);
            RecentSearches = new ObservableCollection<Medicijn>();


            RecentSearches.Add(new Medicijn
            {
                HPKode = 2100533,
                PRKode = 95273,
                GPKode = 134929,
                NMMEMO = "VOTRT4",
                NMMEMO050 = "PAZOT4",
                ATCode = "L01XE11",
                NMNaam = "VOTRIENT TABLET FILMOMHULD 400MG",
                NMNaam050 = "PAZOPANIB TABLET 400MG",
                Naam = "VOTRIENT TABLET FILMOMHULD 400MG - PAZOPANIB TABLET 400MG",
            });
        }

        public ZoekMedicijnViewModel(INavigation navigation, Recept recept) : this()
        {
            _navigation = navigation;

            Recept = recept;
        }

        public ZoekMedicijnViewModel(INavigation navigation, Action<Medicijn, int, string> addRegelToRecept) : this()
        {
            _navigation = navigation;
            _addRegelToRecept = addRegelToRecept;
        }

        private void AddMedicijnToRecept(Medicijn medicijn)
        {
            //_addRegelToRecept.Invoke(medicijn, int.Parse(Aantal), Dosering);

            _addRegelToRecept.Invoke(medicijn, 2, "2");

            Modal.Instance.IsVisible = false;
        }

        public async void SearchMedicijn()
        {
            if(SearchValue == "" || SearchValue == null) return;

            HeaderText = "SEARCH RESULTS";

            IsLoading = true;

            Medicijnen = new ObservableCollection<Medicijn>(
                await _medicijnService.ZoekMedicijn(SearchValue)
            );

            IsLoading = false;

        }
        
        private void CloseOverlay() 
        {
            Modal.Instance.IsVisible = false;
        }
    }
}
