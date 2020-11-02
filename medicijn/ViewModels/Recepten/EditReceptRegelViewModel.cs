using System;
using System.Diagnostics;
using GZIDAL002.Recepten.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using medicijn.Utils;

namespace medicijn.ViewModels.Recepten
{
    public class EditReceptRegelViewModel : BaseViewModel
    {
        public ICommand EditRegelButtonPressed { get; }
        public ICommand CancelButtonPressedCommand { get; }

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
            ReceptRegel = receptRegel;
            EditRegelButtonPressed = new Command(EditRegel);
            CancelButtonPressedCommand = new Command(CancelButtonPressed);
            EditReceptRegel = editReceptRegel;
        }

        private void EditRegel()
        {
            Modal.Instance.IsVisible = false;
            EditReceptRegel(ReceptRegel);
        }

        private void CancelButtonPressed()
        {
            Modal.Instance.IsVisible = false;
        }

    }
}
