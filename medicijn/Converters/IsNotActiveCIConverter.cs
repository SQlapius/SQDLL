using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using GZIDAL002.Recepten.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicijn.Converters
{
    public class IsNotActiveCIConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return true;

            var activeCIs = ((ListView)parameter)
                .ItemsSource
                .Cast<CIPatient>()
                .ToList();
            var currentCI = value as LOV;
            var item = activeCIs.FirstOrDefault(x => x.CICode == currentCI.Id);

            return item == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
