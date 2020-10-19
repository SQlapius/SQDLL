using System;
using System.Diagnostics;
using Xamarin.Forms;
using GZIDAL002.Patienten.Models;
using System.Threading.Tasks;
using medicijn.Utils;
using System.Windows.Input;
using medicijn.Views.Recepten;

namespace medicijn.ViewModels.Patienten
{
    public class ViewPatientViewModel : BaseViewModel 
    {
        
        public ICommand CloseOverlayCommand { get; set; }

        public Patient Patient { get; set; }

        private MakeReceptView _content = new MakeReceptView();
        public MakeReceptView Content 
        {
            get => _content;
               
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public ViewPatientViewModel() { }

        public ViewPatientViewModel(Patient patient)
        {
            Patient = patient;
            CloseOverlayCommand = new Command(CloseOverlay);
            test();
        }

        public async void test()
        {
            //Modal.Instance.IsVisible = true;
            Modal.Instance.Content = new MakeReceptView();
        }

        public void CloseOverlay()
        { 
            Modal.Instance.IsVisible = false;
        }
    }
}
    