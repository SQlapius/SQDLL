using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GZIDAL002.Recepten;
using GZIDAL002.Recepten.Models;
using medicijn.Utils;
using Xamarin.Forms;

namespace medicijn.ViewModels.Recepten
{
    public class DisplayInfoTextViewModel : BaseViewModel
    {
        ReceptService _receptService;

        private WebView _webView;
        public WebView WebView
        {
            get => _webView;
            set
            {
                _webView = value;
                OnPropertyChanged();
            }
        }

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

        public DisplayInfoTextViewModel()
        {
            _receptService = new ReceptService();

            IsLoading = true;
        }

        public DisplayInfoTextViewModel(ContraIndicatie ci) : this()
        {
            GetCIInfoText(ci.CICode);
        }

        public DisplayInfoTextViewModel(Interactie ia) : this()
        {
            GetIAInfoText(ia.IAKode);
        }

        private async void GetIAInfoText(int iaKode)
        {
            var infoTekst = await _receptService.GetAIInfoTekst(iaKode);

            InitWebView(infoTekst);
        }

        private async void GetCIInfoText(int ciCode)
        {
            var infoTekst = await _receptService.GetCIInfoTekst(ciCode);

            InitWebView(infoTekst);
        }

        private void InitWebView(string htmlString)
        {
            var webView = new WebView();
            var webViewSource = new HtmlWebViewSource
            {
                Html = htmlString
            };

            webView.VerticalOptions = LayoutOptions.FillAndExpand;
            webView.HorizontalOptions = LayoutOptions.FillAndExpand;
            webView.Source = webViewSource;
            webView.Navigating += WebView_Navigating;

            WebView = webView;
        }

        private async void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            //Add small delay to avoid flickering
            await Task.Delay(200);

            IsLoading = false;
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Debug.WriteLine("OK");
        }
    }
}
