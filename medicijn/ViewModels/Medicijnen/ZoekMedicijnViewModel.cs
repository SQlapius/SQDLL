using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Medicijnen;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Recepten.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using medicijn.Utils;
using medicijn.Databases;

namespace medicijn.ViewModels.Medicijnen
{
    public class ZoekMedicijnViewModel : BaseViewModel
    {
        INavigation _navigation;
        MedicijnService _medicijnService;
        MedicijnDatabase _medicijnDatabase;

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
            get => _medicijnen; 
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
                Debug.WriteLine(JsonConvert.SerializeObject(value));
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
            _medicijnDatabase = new MedicijnDatabase();

            SearchButtonPressedCommand = new Command(SearchMedicijn);
            CloseOverlayCommand = new Command(CloseOverlay);

            LoadRecentSearches();

        }

        async private void LoadRecentSearches()
        {
            HeaderText = "RECENT SEARCHES";

            Medicijnen = await _medicijnDatabase.GetLast5Medicijn();
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

        private async void AddMedicijnToRecept(Medicijn medicijn)
        {
            //_addRegelToRecept.Invoke(medicijn, int.Parse(Aantal), Dosering);

            _addRegelToRecept.Invoke(medicijn, 2, "2");
            await _medicijnDatabase.StoreMedicijn(medicijn);

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
