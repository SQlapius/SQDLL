using System;
using Xamarin.Forms;
namespace medicijn.Utils
{
    public class Modal: BaseViewModel
    {

        public static Modal Instance { get; private set; }


        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        private ContentView _content;
        public ContentView Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }



        private Modal()
        {
        }

        static Modal() 
        { 
            Instance = new Modal(); 
        }
    }
}
