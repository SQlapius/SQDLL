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
using medicijn.Views.Recepten;
using medicijn.Models;
using System.Collections.Generic;

namespace medicijn.ViewModels.Recepten
{
    public class MakeReceptViewModel : BaseViewModel
    {
        INavigation _navigation;
        ReceptService _receptService;
        PatientService _patientService;

        public ICommand NewReceptLinePressedCommand { get; }
        public ICommand AddButtonPressedCommand { get; }
        public ICommand CancelButtonPressedCommand { get; }
        public ICommand MedAardPressedCommand { get; }
        public ICommand CreateNewReceptPressedCommand { get; }
        public ICommand CIInfoButtonPressedCommand { get; }
        public ICommand IAInfoButtonPressedCommand { get; }

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

        private bool _addMenuDropdownIsOpen = false;
        public bool AddMenuDropdownIsOpen
        {
            get => _addMenuDropdownIsOpen;
            set
            {
                _addMenuDropdownIsOpen = value;
                OnPropertyChanged();
            }
        }

        private bool _receptRegelMenuIsOpen = false;
        public bool ReceptRegelMenuIsOpen
        {
            get => _receptRegelMenuIsOpen;
            set
            {
                _receptRegelMenuIsOpen = value;
                OnPropertyChanged();
            }
        }

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

        private IList<string> _hi;
        public IList<string> Hi
        {
            get => _hi;
            set
            {
                _hi = value;
                OnPropertyChanged();
            }
        }


        private int? _selectedReceptRegel;

        public Patient Patient { get; set; }

        public MakeReceptViewModel()
        {
            _receptService = new ReceptService();
            _patientService = new PatientService();

            NewReceptLinePressedCommand = new Command(OpenMedicinePicker);
            CreateNewReceptPressedCommand = new Command(SubmitRecept);
            CancelButtonPressedCommand = new Command(NavigateBack);
            MedAardPressedCommand = new Command<ContraIndicatie>(ChoosePatientCIAardAction);
            CIInfoButtonPressedCommand = new Command<ContraIndicatie>(NavigateToCIInfoView);
            IAInfoButtonPressedCommand = new Command<Interactie>(NavigateToIAInfoView);
            AddButtonPressedCommand = new Command(AddButtonPressed);
        }

        public MakeReceptViewModel(INavigation navigation, Patient patient) : this()
        {
            _navigation = navigation;

            Recept = new Recept(patient, "Londy");
        }

        public MakeReceptViewModel(INavigation navigation, Recept recept) : this()
        {
            _navigation = navigation;
            Recept = recept;
        }
        private async void ChoosePatientCIAardAction(ContraIndicatie contra)
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

            if (chosenOption == string.Empty)
                return;

            /*
             *  Reset Values
             */
            contra.IsBewaakt = 0;
            contra.IsOnderdrukt = 0;

            var actie = GetCIAardActieCode(chosenOption);
            var status = await _patientService.SavePatientCIAardFlag(
                contra.PcaId,
                actie
            );

            if (actie == "B")
                contra.IsBewaakt = 1;

            if (actie == "H")
                contra.IsOnderdrukt = 1;

        }

        private string[] GetAardActieOptions(ContraIndicatie CI)
        {
            return new string[]
            {
                "Bewaken",
                "Onderdrukken",
                CI.PatCIAardActie == "B" ? "Bewaking stoppen" : null
            };
        }

        private void SubmitRecept()
        {
            Navigator.Instance.Add(
                  new NavPage(
                      $"Confirm Recept #{Recept.Id}",
                      new ConfirmReceptView(Recept)
                  )
              );
        }

        private async void AddRegelToRecept(Medicijn medicijn, int aantal, string dosering)
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

        private void NavigateToIAInfoView(Interactie ia)
        {
            Navigator.Instance.Add(
                  new NavPage(
                      $"{ia.IAKode} - {ia.IAOms}",
                      new DisplayInfoTextView(ia)
                  )
              );
        }

        private void NavigateToCIInfoView(ContraIndicatie ci)
        {
            Navigator.Instance.Add(
                new NavPage(
                    $"{ci.CICode} - {ci.Aard}",
                    new DisplayInfoTextView(ci)
                )
            );
        }

        private void OpenMedicinePicker()
        {
            AddMenuDropdownIsOpen = false;
            Modal.Instance.Content = new ZoekMedicijnView(AddRegelToRecept);
            Modal.Instance.IsVisible = true;
        }

        private void AddButtonPressed()
        {
            AddMenuDropdownIsOpen = !AddMenuDropdownIsOpen;
        }

        public void ClickedReceptRegelMenu(int id)
        {
            if(_selectedReceptRegel == id) 
            {
                ReceptRegelMenuIsOpen = false;
                _selectedReceptRegel = null;
                return;
            }

            ReceptRegelMenuIsOpen = true;
            _selectedReceptRegel = id;
        }

        private void NavigateBack()
        {
            Navigator.Instance.Pop();
        }
    }
}
