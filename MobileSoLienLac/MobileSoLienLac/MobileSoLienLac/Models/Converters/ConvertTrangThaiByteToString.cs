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
                string val = (string)value;
                if (val.Equals("Chờ duyệt"))
                {
                    return Color.Red;
                }
                else if (val.Equals("Đã duyệt"))
                {
                    return Color.Green;
                }
                else
                {
                    return Color.Yellow;
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
