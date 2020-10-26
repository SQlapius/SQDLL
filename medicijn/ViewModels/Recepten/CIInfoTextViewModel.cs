using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GZIDAL002.Recepten;
using GZIDAL002.Recepten.Models;
using medicijn.Utils;
using Xamarin.Forms;

namespace medicijn.ViewModels.Recepten
{
    public class CIInfoTextViewModel : BaseViewModel
    {
        ReceptService _receptService;

        public ContraIndicatie CI { get; set; }

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

        private HtmlWebViewSource _htmlSource;
        public HtmlWebViewSource HtmlSource
        {
            get => _htmlSource;
            set
            {
                _htmlSource = value;
                OnPropertyChanged();
            }
        }

        public CIInfoTextViewModel()
        {
            _receptService = new ReceptService();
        }

        public CIInfoTextViewModel(ContraIndicatie ci) : this()
        {
            CI = ci;
            IsLoading = true;

            GetCIInfoText();
        }
        
        private async void GetCIInfoText()
        {
            IsLoading = true;

            var infoTekst = await _receptService.GetCIInfoTekst(CI.CICode);
            var webViewSource = new HtmlWebViewSource()
            {
                Html = infoTekst
            };

            HtmlSource = webViewSource;
            IsLoading = false;
        }
    }
}
