using System;
using System.Collections.Generic;

namespace medicijn.ViewModels.Recepten
{
    public class DoseringAanpassenViewModel : BaseViewModel
    {
        public List<dynamic> Options { get; set; }

        private int _selectedId;
        public int SelectedId
        {
            get => _selectedId;
            set
            {
                _selectedId = value;
                OnPropertyChanged();
            }
        }

        public DoseringAanpassenViewModel()
        {
            SelectedId = 0;
            Options = new List<dynamic>()
            {
                new { Option = "Elke dag", Id = 0 },
                new { Option = "Om de dag", Id = 1 },
                new { Option = "Specifieke dagen", Id = 2 }
            };
        }
    }
}
