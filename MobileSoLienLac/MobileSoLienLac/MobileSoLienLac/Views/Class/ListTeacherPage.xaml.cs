using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;
using MobileSoLienLac.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Class
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTeacherPage : ContentPage
    {
        ValueDTO<ModelListTeacher> val = new ValueDTO<ModelListTeacher>();

        public ListTeacherPage()
        {
            InitializeComponent();
            Title = "Giáo viên lớp " + App.StudentSeclect.TenLop;
            GetListTeacher();
            if (val.Error != 0)
            {
                StackLayoutMain.Children.Remove(ListViewListTeacher);
            }
            else
            {
                StackLayoutMain.Children.Remove(lblNotify);
                ListViewListTeacher.ItemsSource = val.ListT;
            }
        }

        public async void GetListTeacher()
        {
            val = await new ListTeacherViewModel().GetData(App.StudentSeclect.IDLop);
            if (val.Error != 0)
            {
                await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(val.Error), "OK");
            }
        }
    }
}