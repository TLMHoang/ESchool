using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student.Fee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListFeeByMonth : ContentPage
    {
        public class ItemMonth
        {
            public int ID { get; set; }
            public string NameMonth { get; set; }

            public override string ToString() => NameMonth;

            public ItemMonth(int iD, string nameMonth)
            {
                ID = iD;
                NameMonth = nameMonth;
            }
        }

        public List<ItemMonth> LstItemMonths = new List<ItemMonth>();

        public ListFeeByMonth()
        {
            InitializeComponent();

            Title = "Học phí - " + App.StudentSeclect.Ten;

            for (int i = 1; i < 13; i++)
            {
                LstItemMonths.Add(new ItemMonth(i, "Tháng " + i));
            }

            PickerMonth.ItemsSource = LstItemMonths;
        }
    }
}