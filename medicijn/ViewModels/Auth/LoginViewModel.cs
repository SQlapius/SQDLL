using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using medicijn.Views.Patienten;
using Xamarin.Forms;

namespace medicijn.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginButtonPressedCommand { get; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool _isLoading = false;
        public bool IsLoading 
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            } 
        }

        public LoginViewModel()
        {
            LoginButtonPressedCommand = new Command(LoginbuttonPressed);
        }

        async private void LoginbuttonPressed()
        {
            IsLoading = true;
            await Task.Delay(2000);
            Application.Current.MainPage = new NavigationPage(new ZoekPatientView());
            IsLoading = false;
        }
    }
}
