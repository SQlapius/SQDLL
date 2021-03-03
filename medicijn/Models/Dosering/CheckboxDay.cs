using System;
namespace medicijn.Models.Dosering
{
    public class CheckboxDay : BaseViewModel
    {
        public string Weekday { get; set; }

        public int Id { get; set; }

        private bool _selected;
        public bool Selected 
        { 
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabled = false;
        public bool IsEnabled 
        { 
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        } 
    }
}
