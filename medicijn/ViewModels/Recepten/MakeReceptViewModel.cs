using System;
using System.Diagnostics;
using System.Windows.Input;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten.Models;
using medicijn.Views.Medicijnen;
using Xamarin.Forms;
using medicijn.Utils;
using GZIDAL002.Recepten;
using GZIDAL002.Medicijnen.Models;
using Newtonsoft.Json;

namespace medicijn.ViewModels.Recepten
{
    public class MakeReceptViewModel : BaseViewModel
    {
        INavigation _navigation;
        ReceptService _receptService;

        public ICommand NewReceptLinePressedCommand { get; set; }

        private Recept _recept;
        public Recept Recept
        {
            get => _recept;
            set
            {
                _recept = value;
                OnPropertyChanged();
            }
        }

        public Patient Patient { get; set; }

        public MakeReceptViewModel()
        {
            NewReceptLinePressedCommand = new Command(OpenMedicinePicker);
        }

        public MakeReceptViewModel(INavigation navigation, Patient patient) : this()
        {
            _navigation = navigation;
            _receptService = new ReceptService();

            Recept = new Recept(patient, "Londy");
        }

        public async void AddRegelToRecept(Medicijn medicijn, int aantal, string dosering)
        {
            Recept = await _receptService.AddReceptRegel(
                Recept,
                medicijn,
                aantal,
                dosering
            );
        }

        private async void OpenMedicinePicker()
        {
            //await _navigation.PushModalAsync(new ZoekMedicijnView(Recept));
            Modal.Instance.IsVisible = true;
            Modal.Instance.Content = new ZoekMedicijnView(AddRegelToRecept);
        }
    }
}
