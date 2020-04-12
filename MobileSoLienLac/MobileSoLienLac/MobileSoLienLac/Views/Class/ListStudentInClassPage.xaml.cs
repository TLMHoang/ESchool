using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;
using MobileSoLienLac.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStudentInClassPage : ContentPage
    {
        public ValueDTO<ModelListStudent> val { get; set; }

        public ListStudentInClassPage()
        {
            InitializeComponent();
            Title = "Danh sách lớp " + App.StudentSeclect.TenLop;
            LoadListStudent();
            if (val.Error != 0)
            {
                StackLayoutMain.Children.Remove(ListViewListStudent);
            }
            else
            {
                StackLayoutMain.Children.Remove(lblNotify);
                ListViewListStudent.ItemsSource = val.ListT;
            }
        }


        public async void LoadListStudent()
        {
            val = await new ListStudentInClassViewModel().GetData(App.StudentSeclect.IDLop);
            if (val.Error != 0)
            {
                await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(val.Error), "OK");
            }
        }
    }
}