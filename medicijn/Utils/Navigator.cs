using System;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace medicijn.Utils
{
    public class Navigator : BaseViewModel
    {
        public static Navigator Instance { get; private set; }

        private View MainContent { get; set; }
        public ContentView MainView { get; set; }

        public ObservableCollection<ContentView> Pages { get; set; }

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

        private Navigator()
        {
        }

        static Navigator()
        {
            Instance = new Navigator();
        }

        public void SetMainPage(ContentView contentView)
        { 
            MainView = contentView;
            MainContent = contentView.Content;
            Pages = new ObservableCollection<ContentView>();
        }

        public void Add(ContentView view) 
        {
            ShowBackButton = true;
            Pages.Add(view);

            MainView.Content = view; 
        }

        public void Pop() 
        {
            if(Pages.Count <= 1) 
            {
                MainView.Content = MainContent;
                ShowBackButton = false;
            }
            else
            { 
                MainView.Content = Pages[Pages.Count - 2];
            }

            Pages.RemoveAt(Pages.Count - 1);
            SetTitle("");
        }

        public void SetTitle(string title)
        {
            CurrentTitle = title;
            return;
        }
    }
}
