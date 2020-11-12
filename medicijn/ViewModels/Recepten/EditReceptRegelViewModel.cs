using System;
using System.Diagnostics;
using GZIDAL002.Recepten.Models;
using GZIDAL002.Recepten;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using medicijn.Utils;
using Dosering = GZIDAL002.Recepten.Models.Dosering;



namespace medicijn.ViewModels.Recepten
{
    public class EditReceptRegelViewModel : BaseViewModel
    {
        ReceptService _receptService;
        DoseringTabellen _doseringTabellen;

        public ICommand EditRegelButtonPressed { get; }
        public ICommand CancelButtonPressedCommand { get; }

        private string _doseringString { get; set; }
        public string DoseringString 
        { 
            get => _doseringString;
            set
            {
                _doseringString = value;
                OnPropertyChanged();
            }
        }

        public Action<ReceptRegel> EditReceptRegel { get; }

        private ReceptRegel _receptRegel;
        public ReceptRegel ReceptRegel
        {
            get => _receptRegel; 
            set
            {
                _receptRegel = value;
                OnPropertyChanged();
            }
        }

        public EditReceptRegelViewModel(ReceptRegel receptRegel, Action<ReceptRegel> editReceptRegel)
        {
            _receptService = new ReceptService();
            test();
            ReceptRegel = receptRegel;
            EditRegelButtonPressed = new Command(EditRegel);
            CancelButtonPressedCommand = new Command(CancelButtonPressed);
            EditReceptRegel = editReceptRegel;
        }

        async private void test()
        {
            _doseringTabellen = await _receptService.GetDoseringTables();
        }

        private void EditRegel()
        {
            var test = new Dosering(ReceptRegel.Dosering, _doseringTabellen);
            DoseringString = test.CodeAsString;

            //Modal.Instance.IsVisible = false;
            //EditReceptRegel(ReceptRegel);
        }

        private void CancelButtonPressed()
        {
            Modal.Instance.IsVisible = false;
        }

    }
}
