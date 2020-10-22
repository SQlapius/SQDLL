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
using GZIDAL002.Patienten;

namespace medicijn.ViewModels.Recepten
{
    public class MakeReceptViewModel : BaseViewModel
    {
        INavigation _navigation;
        ReceptService _receptService;
        PatientService _patientService;

        public ICommand NewReceptLinePressedCommand { get; }
        public ICommand CancelButtonPressedCommand { get; }
        public ICommand MedAardPressedCommand { get; }
        public ICommand CreateNewReceptPressedCommand { get; }

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

        private Recept _recept;
        public Recept Recept
        {
            get => _recept;
            set
            {
                Debug.WriteLine("HALLO?");

                _recept = value;
                OnPropertyChanged();
            }
        }

        public Patient Patient { get; set; }

        public MakeReceptViewModel()
        {
            _receptService = new ReceptService();
            _patientService = new PatientService();

            NewReceptLinePressedCommand = new Command(OpenMedicinePicker);
            CreateNewReceptPressedCommand = new Command(SubmitRecept);
            CancelButtonPressedCommand = new Command(NavigateBack);
            MedAardPressedCommand = new Command<ContraIndicatie>(ChoosePatientCIAardAction);
        }

        public MakeReceptViewModel(INavigation navigation, Patient patient) : this()
        {
            _navigation = navigation;

            Recept = new Recept(patient, "Londy");
        }

        public async void ChoosePatientCIAardAction(ContraIndicatie contra)
        {
            if (contra.PatCIAardActie == "B")
                return;

            var chosenOption = await Application.Current.MainPage.DisplayActionSheet(
                "Kies je actie voor " + contra.Aard,
                "cancel",
                null,
                "bewaken",
                "onderdrukken"
            );
            var actie = GetCIAardActieCode(chosenOption);
            var status = await _patientService.SavePatientCIAardFlag(
                contra.PcaId,
                actie
            );

            contra.PatCIAardActie = actie;
        }

        public string[] GetAardActieOptions(ContraIndicatie CI)
        {
            return new string[]
            {
                "Bewaken",
                "Onderdrukken",
                CI.PatCIAardActie == "B" ? "Bewaking stoppen" : null
            };
        }

        public async void SubmitRecept()
        {
            var ok = await _receptService.SaveRecept(Recept);

            Debug.WriteLine(JsonConvert.SerializeObject(ok));
        }

        public async void AddRegelToRecept(Medicijn medicijn, int aantal, string dosering)
        {
            IsLoading = true;
            Recept = await _receptService.AddReceptRegel(
                Recept,
                medicijn,
                aantal,
                dosering
            );

            IsLoading = false;
        }

        private string GetCIAardActieCode(string actie)
        {
            if (actie == "bewaken")
                return "B";

            if (actie == "onderdrukken")
                return "H";

            if (actie == "bewaking stoppen")
                return "S";

            return "";
        }

        private void OpenMedicinePicker()
        {
            Modal.Instance.IsVisible = true;
            Modal.Instance.Content = new ZoekMedicijnView(AddRegelToRecept);
        }

        private void NavigateBack()
        {
            Navigator.Instance.Pop();
        }
    }
}
