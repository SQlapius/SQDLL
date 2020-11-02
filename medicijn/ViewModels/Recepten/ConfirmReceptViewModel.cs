using System;
using System.Diagnostics;
using GZIDAL002.Recepten.Models;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using medicijn.Utils;
using medicijn.Views.Recepten;

namespace medicijn.ViewModels.Recepten
{
    public class ConfirmReceptViewModel : BaseViewModel
    {
        public ICommand EditReceptRegel { get; }
        public ICommand CancelButtonPressedCommand { get; }


        private int _selectedReceptRegel;

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

        public ConfirmReceptViewModel(Recept recept)
        {
            Recept = recept;
            EditReceptRegel = new Command<ReceptRegel>(OpenEditReceptRegelModal);
            CancelButtonPressedCommand = new Command(CancelButtonPressed);

        }

        private void  OpenEditReceptRegelModal(ReceptRegel regel)
        {
            _selectedReceptRegel = Recept.ReceptRegels.IndexOf(regel);
            Modal.Instance.Content = new EditReceptRegelView(regel, EditReceptRegels);
            Modal.Instance.IsVisible = true;
        }

        private void  EditReceptRegels(ReceptRegel regel)
        {
            Recept.ReceptRegels[_selectedReceptRegel] = regel;
        }

        private void CancelButtonPressed()
        {
            Navigator.Instance.Pop();
        }
    }
}
