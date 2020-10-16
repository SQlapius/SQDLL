using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GZIDAL002.Medicijnen;
using GZIDAL002.Medicijnen.Models;
using Xamarin.Forms;

namespace medicijn.ViewModels.Medicijnen
{
    public class ZoekMedicijnViewModel : BaseViewModel
    {
        INavigation _navigation;
        MedicijnService _medicijnService;

        public ICommand SearchButtonPressedCommand { get; }

        public string SearchValue { get; set; }

        public Medicijn SelectedPatient
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                //    NavigateToPatientView(value);
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

        public ZoekMedicijnViewModel()
        {
            _medicijnService = new MedicijnService();

            SearchButtonPressedCommand = new Command(SearchMedicijn);
        }

        public ZoekMedicijnViewModel(INavigation navigation) : this()
        {
            _navigation = navigation;
        }

        private async void SearchMedicijn()
        {
            Medicijnen = new ObservableCollection<Medicijn>(
                await _medicijnService.ZoekMedicijn(SearchValue)
            );
        }
    }
}
