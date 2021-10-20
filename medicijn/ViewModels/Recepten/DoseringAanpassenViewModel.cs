using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GZIDAL002.Recepten.Models;
using medicijn.Models.Dosering;
using medicijn.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace medicijn.ViewModels.Recepten
{
    public class DoseringAanpassenViewModel : BaseViewModel
    {

        public Command DecrementMedicineAmountCommand { get; }
        public Command IncrementMedicineAmountCommand { get; }
        public Command BackButtonPressedCommand { get; }
        public Command ClickedOnAWeekdayCommand { get; }

        public List<DoseringOption> Options { get; set; }
        public List<DoseringTakeInOption> TakeInOptions { get; set; }
        public List<CheckboxDay> Weekdays { get; set; }
        public List<UsageOption> UsageOptions { get; set; }
        public List<RepeatedOption> RepeatedOptions { get; set; }

        private int _medicineAmount = 1;
        public int MedicineAmount
        {
            get => _medicineAmount;
            set
            {
                _medicineAmount = value;
                OnPropertyChanged();
            }
        }

        private int _selectedId;
        public int SelectedId
        {
            get => _selectedId;
            set
            {
                _selectedId = value;
                UpdateCheckboxes();
                OnPropertyChanged();
            }
        }

        private int _usageOptionsSelectedId;
        public int UsageOptionsSelectedId
        {
            get => _usageOptionsSelectedId;
            set
            {
                _usageOptionsSelectedId = value;
                OnPropertyChanged();
            }
        }

        private int _repeatedOptionsSelectedId;
        public int RepeatedOptionsSelectedId
        {
            get => _repeatedOptionsSelectedId;
            set
            {
                _repeatedOptionsSelectedId = value;
                OnPropertyChanged();
            }
        }

        private int _selectedTakeInId;
        public int SelectedTakeInId
        {
            get => _selectedTakeInId;
            set
            {
                _selectedTakeInId = value;
                OnPropertyChanged();
            }
        }

        private int _receptRegelId;
        public int ReceptRegelId
        {
            get => _receptRegelId;
            set
            {
                _receptRegelId = value;
                OnPropertyChanged();
            }
        }

        private string _repeatedInput;
        public string RepeatedInput
        {
            get => _repeatedInput;
            set
            {
                _repeatedInput = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> _selectedIds = new ObservableCollection<int>();
        public ObservableCollection<int> SelectedIds
        {
            get => _selectedIds;
            set
            {
                _selectedIds = value;
                ShowTakeInInputs();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DoseringTakeInOption> _selectedOptions;
        public ObservableCollection<DoseringTakeInOption> SelectedOptions 
        {
            get => _selectedOptions;
            set
            {
                _selectedOptions = value;
                OnPropertyChanged();
            }
        }

        public DoseringAanpassenViewModel()
        {

            DecrementMedicineAmountCommand = new Command(DecrementDosering);
            IncrementMedicineAmountCommand = new Command(IncrementDosering);
            BackButtonPressedCommand = new Command(BackButtonPressed);
            ClickedOnAWeekdayCommand = new Command<int>(WeekdayPressed);

            SelectedId = 0;
            UsageOptionsSelectedId = 0;
            RepeatedOptionsSelectedId = 0;


            Options = new List<DoseringOption>()
            {
                new DoseringOption { Option = "Elke dag", Id = 0 },
                new DoseringOption { Option = "Om de dag", Id = 1 },
                new DoseringOption { Option = "Specifieke dagen", Id = 2 },
                new DoseringOption { Option = "Om X dagen", Id = 3 },
                new DoseringOption { Option = "Zo Nodig", Id = 4 },
                new DoseringOption { Option = "Vrije Invoer", Id = 5 },
            };

            TakeInOptions = new List<DoseringTakeInOption>()
            {
                new DoseringTakeInOption { Option = "1u voor het ontbijt", Id = 0, Amount = "" },
                new DoseringTakeInOption { Option = "bij ontbijt", Id = 1, Amount = "" },
                new DoseringTakeInOption { Option = "2u na of 1u voor", Id = 2, Amount = "" },
                new DoseringTakeInOption { Option = "Bij middag eten", Id = 3, Amount = "" },
                new DoseringTakeInOption { Option = "2u na of 1u voor", Id = 4, Amount = "" },
                new DoseringTakeInOption { Option = "Bij avondeten", Id = 5, Amount = "" },
                new DoseringTakeInOption { Option = "voor het slapen", Id = 6, Amount = "" },
                new DoseringTakeInOption { Option = "afwijkend", Id = 7, Amount = "" }
            };

            Weekdays = new List<CheckboxDay>()
            {
                new CheckboxDay { Weekday = "Maandag", Selected = false, Id = 0},
                new CheckboxDay { Weekday = "Dinsdag", Selected = false, Id = 1},
                new CheckboxDay { Weekday = "Woensdag", Selected = false, Id = 2},
                new CheckboxDay { Weekday = "Donderdag", Selected = false, Id = 3},
                new CheckboxDay { Weekday = "Vrijdag", Selected = false, Id = 4},
                new CheckboxDay { Weekday = "Zaterag", Selected = false, Id = 5},
                new CheckboxDay { Weekday = "Zondag", Selected = false, Id = 6},
            };

            SelectedIds = new ObservableCollection<int>()
            {
                1,
                0
            };

            UsageOptions = new List<UsageOption>()
            {
                new UsageOption { Option = "Inwendig", Id = 0},
                new UsageOption { Option = "Uitwendig", Id = 1}
            };

            RepeatedOptions = new List<RepeatedOption>()
            {
                new RepeatedOption { Option = "Ja", Id = 0},
                new RepeatedOption { Option = "Nee", Id = 1}
            };
        }

        public DoseringAanpassenViewModel(int receptRegelId, Action<ReceptRegel> action) : this()
        {
            ReceptRegelId = receptRegelId;
        }

        private void UpdateCheckboxes()
        { 
            if(Weekdays == null) return;

            switch (SelectedId)
            {
                case 0:
                    CheckEveryDay();
                    break;

                case 1:
                    CheckOmDeDag();
                    break;

                case 3:
                    OmXdagenPressed();
                    break;

                default:
                    ResetCheckboxes();
                    break;
            }
        }

        private void WeekdayPressed(int clickedDayId)
        {
            if(SelectedId != 2)
                return; 

            var pressedDay  = Weekdays.Where(x => x.Id == clickedDayId).FirstOrDefault();
            int index = Weekdays.IndexOf(pressedDay);

            pressedDay.Selected = !pressedDay.Selected;
            Weekdays[index] = pressedDay;

            if (Weekdays.All(x => x.Selected == true))
                SelectedId = 0;

        }

        private void CheckEveryDay()
        { 
            foreach(var day in Weekdays) 
            {
                day.Selected = true;
                day.IsEnabled = false;
            }
        }

        private void CheckOmDeDag()
        { 
            foreach(var day in Weekdays) 
            {
                day.Selected = day.Id % 2 == 0;
                day.IsEnabled = false;
            }
        }

        async private void OmXdagenPressed()
        {
            try
            {
                DisableCheckboxes();
                var amountofdays = await Application
                      .Current
                      .MainPage
                      .DisplayPromptAsync(
                          "Om x dagen",
                          "Om de hoeveel dagen met de patient deze medicatie innemen",
                          initialValue: ""
                      );

                if (string.IsNullOrEmpty(amountofdays))
                    return;

                var newValue = int.Parse(amountofdays);
            }
            catch(FormatException)
            {
                DisplayAlert.PromptError("Value needs to be numeric");
                SelectedId = 0; 
            }
            catch
            {
                DisplayAlert.PromptError("Something went wrong");
                SelectedId = 0; 
            }
        }

        private void ResetCheckboxes()
        { 
            foreach(var day in Weekdays) 
            {
                day.Selected = false;
                day.IsEnabled = true;
            }
        }

        private void DisableCheckboxes()
        { 
            foreach(var day in Weekdays) 
            {
                day.Selected = false;
                day.IsEnabled = false;
            }
        }

        private void DecrementDosering()
        {
            if(MedicineAmount > 1)
            { 
                MedicineAmount--;
            }
        }

        private void IncrementDosering()
        {
            MedicineAmount++;
        }

        private void BackButtonPressed()
        {
            Modal.Instance.CloseModal();
        }

        private void ShowTakeInInputs()
        {
            SelectedOptions = new ObservableCollection<DoseringTakeInOption>();

            for (var i = 0; i < SelectedIds.Count; i++)
            {
                SelectedOptions.Add(TakeInOptions
                    .Where(x => x.Id == SelectedIds[i]).FirstOrDefault());
            }

            Debug.WriteLine(JsonConvert.SerializeObject(SelectedOptions));
        }
    }
}
