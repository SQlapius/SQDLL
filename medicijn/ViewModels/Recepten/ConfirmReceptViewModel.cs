using System;
using System.Diagnostics;
using GZIDAL002.Recepten.Models;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;
using medicijn.Utils;
using medicijn.Views.Recepten;
using GZIDAL002.Recepten;

namespace medicijn.ViewModels.Recepten
{
    public class ConfirmReceptViewModel : BaseViewModel
    {
        ReceptService _receptService;

        public ICommand EditReceptRegel { get; }
        public ICommand CancelButtonPressedCommand { get; }
        public ICommand VoorSchrijvenButtonPressedCommand { get; }

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

        public ConfirmReceptViewModel()
        {
            _receptService = new ReceptService();

            EditReceptRegel = new Command<ReceptRegel>(OpenEditReceptRegelModal);
            CancelButtonPressedCommand = new Command(CancelButtonPressed);
            VoorSchrijvenButtonPressedCommand = new Command(CommitRecept);
        }

        public ConfirmReceptViewModel(Recept recept) : this()
        {
            Recept = recept;
        }

        private void OpenEditReceptRegelModal(ReceptRegel regel)
        {
            _selectedReceptRegel = Recept.ReceptRegels.IndexOf(regel);

            Modal.Instance.Content = new EditReceptRegelView(regel, EditReceptRegels);
            Modal.Instance.IsVisible = true;
        }

        private void EditReceptRegels(ReceptRegel regel)
        {
            Recept.ReceptRegels[_selectedReceptRegel] = regel;
        }

        private async void CommitRecept()
        {
            await _receptService.SaveRecept(Recept);
        }

        private void CancelButtonPressed()
        {
            Navigator.Instance.Pop();
        }
    }
}
