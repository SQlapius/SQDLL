using System;
using System.Windows.Input;
using GZIDAL002.Recepten.Models;
using Xamarin.Forms;

namespace medicijn.ViewModels.Recepten
{
    public class MakeReceptViewModel : BaseViewModel
    {
        INavigation _navigation;

        public ICommand NewReceptLinePressed { get; set; }

        public Recept Recept { get; set; }

        public MakeReceptViewModel()
        {
            NewReceptLinePressed = new Command(OpenMedicinePicker);
        }

        public MakeReceptViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        private async void OpenMedicinePicker()
        {
            await _navigation.PushModalAsync(new ContentPage());
        }
    }
}
