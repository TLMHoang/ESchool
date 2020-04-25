using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileSoLienLac.Models.Converters
{
    class ConvertPointToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double val = (double)value;
                if (val >= 8)
                {
                    return Color.LimeGreen;
                }
                else if (val >= 6.5)
                {
                    return Color.LightGreen;
                }
                else if (val >= 5)
                {
                    return Color.Orange;
                }
                else
                {
                    return Color.Red;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
