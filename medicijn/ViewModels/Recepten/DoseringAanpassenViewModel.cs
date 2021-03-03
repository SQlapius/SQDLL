using System;
using System.Collections.Generic;
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

        public List<dynamic> Options { get; set; }
        public List<dynamic> TakeInOptions { get; set; }
        public List<CheckboxDay> Weekdays { get; set; }

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


        public DoseringAanpassenViewModel()
        {

            DecrementMedicineAmountCommand = new Command(DecrementDosering);
            IncrementMedicineAmountCommand = new Command(IncrementDosering);
            BackButtonPressedCommand = new Command(BackButtonPressed);
            ClickedOnAWeekdayCommand = new Command<int>(WeekdayPressed);

            SelectedId = 0;
            Options = new List<dynamic>()
            {
                new { Option = "Elke dag", Id = 0 },
                new { Option = "Om de dag", Id = 1 },
                new { Option = "Specifieke dagen", Id = 2 }
            };

            TakeInOptions = new List<dynamic>()
            {
                new { Option = "Ochtend", Id = 0 },
                new { Option = "Voor het eten", Id = 1 },
                new { Option = "Na het eten", Id = 2 },
                new { Option = "Avond", Id = 3 },
                new { Option = "Voor het slapen", Id = 4 }
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

        private void ResetCheckboxes()
        { 
            foreach(var day in Weekdays) 
            {
                day.Selected = false;
                day.IsEnabled = true;
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
    }
}
