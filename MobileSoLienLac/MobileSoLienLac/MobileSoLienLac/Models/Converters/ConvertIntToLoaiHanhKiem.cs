using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MobileSoLienLac.Models.Converters
{
    class ConvertIntToLoaiHanhKiem : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int val = (int)value;
                return (val == -1)? "Chưa xếp hạnh" : App.lstLoaiHanhKiems.FirstOrDefault(p => p.ID == val).TenHanhKiem;
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
