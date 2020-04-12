using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileSoLienLac.Models.Converters
{
    public class ConvertTrangThaiByteToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                byte val = (byte)value;
                return (val == 1)?"Đã duyệt":"Chưa duyệt";
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
