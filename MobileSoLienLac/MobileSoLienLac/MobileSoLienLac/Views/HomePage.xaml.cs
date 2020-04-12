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
using MobileSoLienLac.Views.Student.Fee;
using MobileSoLienLac.Views.Student.RollCall;

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
            await Navigation.PushAsync(new ListTeacherPage());
        }

        private async void BtnStudent_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoStudent());
        }

        private async void BtnFee_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListFeeByMonth());
        }

        private async void BtnListStudent_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListStudentInClassPage());
        }

        private void BtnSchedule_OnClicked(object sender, EventArgs e)
        {
            
        }

        //Event Xin phép nghỉ học
        private async void BtnRRollCall_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListRRollCall());
        }

        private async void BtnListRollCall_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListRollCallPage());
        }
    }
}