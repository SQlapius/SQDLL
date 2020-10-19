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

        public ZoekMedicijnViewModel()
        {
            _medicijnService = new MedicijnService();

            SearchButtonPressedCommand = new Command(SearchMedicijn);

            CloseOverlayCommand = new Command(CloseOverlay);
        }

        public ZoekMedicijnViewModel(INavigation navigation, Recept recept) : this()
        {
            _navigation = navigation;

            Recept = recept;
        }

        private async void AddMedicijnToRecept(Medicijn medicijn)
        {
            Recept.AddRegel(
                medicijn,
                int.Parse(Aantal),
                Dosering
            );

            await _navigation.PopModalAsync();
        }

        private async void SearchMedicijn()
        {
            Medicijnen = new ObservableCollection<Medicijn>(
                await _medicijnService.ZoekMedicijn(SearchValue)
            );
        }
        
        private void CloseOverlay() 
        {
            Modal.Instance.IsVisible = false;
        }
    }
}
