using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GZIDAL002.Global
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
