using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Views.Converters
{
    public class TextExpanderConverter : IValueConverter
    {
        private const int MAX_SIZE = 15; 

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                if (text.Length < MAX_SIZE)
                {
                    var counter = MAX_SIZE - text.Length;
                    for (int i = 0; i < counter; i++)
                    {
                        text += " ";
                    }
                }

                return text;
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
