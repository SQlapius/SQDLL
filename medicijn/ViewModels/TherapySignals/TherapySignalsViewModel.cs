using System;
namespace medicijn.ViewModels.TherapySignals
{
    public class TherapySignalsViewModel : BaseViewModel
    {
        private int _tabIndex;
        public int TabIndex
        {
            get => _tabIndex;
            set
            {
                _tabIndex = value;
                OnPropertyChanged();
            }
        }

        public TherapySignalsViewModel()
        {
        }
    }
}
