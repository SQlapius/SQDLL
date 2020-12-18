using System;
using System.Collections.Generic;
using System.Diagnostics;
using GZIDAL002.Patienten.Models;
using medicijn.Models;
using medicijn.Views.Patienten;
using Xamarin.Forms;

namespace medicijn.ViewModels.Main
{
    public class HomeViewModel : BaseViewModel
    {
        INavigation _navigation;

        public List<HomeActionItem> ActionItems { get; set; }

        private List<Patient> _recentPatients;
        public List<Patient> RecentPatients
        {
            get => _recentPatients; 
            set
            {
                _recentPatients = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            InitActionItems();
            GetRecentPatients();
        }

        public HomeViewModel(INavigation navigation) : this()
        {
            _navigation = navigation;
        }

        private void GetRecentPatients()
        {
            RecentPatients = new List<Patient>()
            {
                new Patient() {  Naam = "Zhong", Sex = "V", Sedula = "1999051402" },
                new Patient() {  Naam = "Zhong", Sex = "V", Sedula = "1999051402" },
                new Patient() {  Naam = "Zhong", Sex = "V", Sedula = "1999051402" },
                new Patient() {  Naam = "Zhong", Sex = "V", Sedula = "1999051402" },
            };
        }

        private void InitActionItems()
        {
            ActionItems = new List<HomeActionItem>()
            {
                new HomeActionItem
                {
                    Title = "Patienten",
                    Icon = "\uf0c0",
                    Color = Color.FromHex("#019999"),
                    Command = new Command(async() => await _navigation.PushAsync(new ZoekPatientView()))
                },
                new HomeActionItem
                {
                    Title = "Settings",
                    Icon = "\uf013",
                    Color = Color.FromHex("#1EA8DE"),
                    Command = new Command(() => Debug.WriteLine("OK"))
                },
                new HomeActionItem
                {
                    Title = "Uitloggen",
                    Icon = "\uf08b",
                    Color = Color.FromHex("#54C6DB"),
                    Command = new Command(() => Debug.WriteLine("OK"))
                }
            };
        }
    }
}
