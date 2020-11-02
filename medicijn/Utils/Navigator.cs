using System;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;
using medicijn.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace medicijn.Utils
{
    public class Navigator : BaseViewModel
    {
        public static Navigator Instance { get; private set; }

        private View MainContent { get; set; }
        public ContentView MainView { get; set; }

        public ObservableCollection<NavPage> Pages { get; set; }

        private string _currentTitle = "";
        public string CurrentTitle
        {
            get => _currentTitle;
            set
            {
                _currentTitle = value;
                OnPropertyChanged();
            }
        }

        private bool _showHeader = false;
        public bool ShowHeader
        {
            get => _showHeader;
            set
            {
                _showHeader = value;
                OnPropertyChanged();
            }
        }

        private bool _showBackButton = false;
        public bool ShowBackButton
        {
            get => _showBackButton;
            set
            {
                _showBackButton = value;
                OnPropertyChanged();
            }
        }
    
        private Navigator() { }

        static Navigator()
        {
            Instance = new Navigator();
        }

        public void SetMainPage(ContentView contentView)
        { 
            MainView = contentView;
            MainContent = contentView.Content;
            Pages = new ObservableCollection<NavPage>();
        }

        public void Add(NavPage page) 
        {
            ShowBackButton = true;
            ShowHeader = true;
            Pages.Add(page);

            MainView.Content = page.Content;
            CurrentTitle = page.PageTitle;
        }

        public async void Pop() 
        {
            if(Pages.Count <= 1) 
            {
                ShowBackButton = false;
                ShowHeader = false;
                await Task.Delay(100);
                MainView.Content = MainContent;
                CurrentTitle = "";
            }
            else
            {
                var page = Pages[Pages.Count - 2];

                MainView.Content = page.Content;
                CurrentTitle = page.PageTitle;
            }

            Pages.RemoveAt(Pages.Count - 1);
        }

        public void SetTitle(string title)
        {
            CurrentTitle = title;
        }
    }
}
