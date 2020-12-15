using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace medicijn.Converters
{
    public class BackgroundColorConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Color.White;

            var index = ((ListView)parameter).ItemsSource.Cast<object>().ToList().IndexOf(value);

            return index % 2 == 0 ? Color.FromHex("#1EA8DE") : Color.FromHex("#019999");
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
