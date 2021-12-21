using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Views.Converters
{
    internal class BoolInversionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status)
            {
                return !status;
            }

            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status)
            {
                return !status;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
