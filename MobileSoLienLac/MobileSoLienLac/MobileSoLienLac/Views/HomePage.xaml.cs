using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Annotations;
using MobileSoLienLac.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;
using MobileSoLienLac.Views;
using MobileSoLienLac.ViewModels;
using MobileSoLienLac.Views.Class;

namespace MobileSoLienLac.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        HomeViewModel viewModel;

        
        
        public ItemsPage()
        {
            InitializeComponent();


            BindingContext = viewModel = new HomeViewModel(BrowseItemsPage.Height);

            PickerChooseStudent.ItemsSource = App.lstStudents;
            PickerChooseStudent.SelectedIndex = 0;
            PickerChooseStudent.SelectedIndexChanged += (sender, e) =>
            {
                App.StudentSeclect = (ThongTinHS)PickerChooseStudent.SelectedItem;
            };

        }

        private async void BtnNotify_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotifyPage());
        }

        private async void BtnListTeacher_OnClicked(object sender, EventArgs e)
        {
            ModelListTeacher val = new ModelListTeacher();
            DataTable dt = await val.GetData(App.StudentSeclect.IDLop);

            if (dt.Columns.Count == 1)
            {
                await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
            }
            else
            {
                await Navigation.PushAsync(new ListTeacherPage(val.GetData(dt), "Giáo viên lớp " + App.StudentSeclect.TenLop));
            }
        }

        private async void BtnStudent_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoStudent());
        }

        private void BtnFee_OnClicked(object sender, EventArgs e)
        {
            
        }

        private async void BtnListStudent_OnClicked(object sender, EventArgs e)
        {
            ListStudentInClassViewModel _value = await new ListStudentInClassViewModel().GetData(App.StudentSeclect.IDLop);
            if (_value.Message.Length == 0)
            {
                _value.Message = "Danh sách lớp " + App.StudentSeclect.TenLop;
                await Navigation.PushAsync(new ListStudentInClassPage(_value));
            }
            else
            {
                await DisplayAlert("Thông báo", _value.Message, "OK");
            }
        }

        private void BtnSchedule_OnClicked(object sender, EventArgs e)
        {
            
        }
    }
}