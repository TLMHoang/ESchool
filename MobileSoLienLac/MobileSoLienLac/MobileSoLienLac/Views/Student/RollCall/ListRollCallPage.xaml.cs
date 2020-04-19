using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student.RollCall
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListRollCallPage : ContentPage
    {
        private List<DiemDanh> lst = new List<DiemDanh>();
        public ListRollCallPage()
        {
            GetData();
            InitializeComponent();
            if (lst.Count == 0)
            {
                StackLayoutMain.Children.Remove(ListViewListRollCall);

            }
            else
            {
                ListViewListRollCall.ItemsSource = lst;
                StackLayoutMain.Children.Remove(lblNotify);
            }
        }

        public async void GetData()
        {
            ValueDTO<DiemDanh> val = await new DiemDanh().GetData(App.StudentSeclect.ID);
            if (val.Error == 0)
            {
                lst = val.ListT;
                lst.Sort(new SortDiemDanh());
                lst.Reverse();
            }
            else
            {
                await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(val.Error), "OK");
            }
        }
    }
}