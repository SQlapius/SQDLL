using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace medicijn.Models
{
    public class HomeActionItem
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public Color Color { get; set; }

        public ICommand Command { get; set; }
    }
}
