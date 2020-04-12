using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileSoLienLac.Models.Converters
{
    public class ConvertByteToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                byte val = (byte)value;
                Color cl;
                return (val == 0) ? Color.Red: Color.LimeGreen;
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
