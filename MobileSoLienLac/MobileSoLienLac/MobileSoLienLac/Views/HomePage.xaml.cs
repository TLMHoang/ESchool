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
using MobileSoLienLac.Views.Student;
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
        FunctionOrder funcBHYT = new FunctionOrder();
        private bool bhyt = true;

        public ItemsPage()
        {
            InitializeComponent();

            CheckbtnBHYT();
            EditNameBtnHealthInsurance();

            BindingContext = viewModel = new HomeViewModel(BrowseItemsPage.Height);

            PickerChooseStudent.ItemsSource = App.lstStudents;
            PickerChooseStudent.SelectedIndex = 0;
            PickerChooseStudent.SelectedIndexChanged += (sender, e) =>
            {
                App.StudentSeclect = (ThongTinHS)PickerChooseStudent.SelectedItem;
                EditNameBtnHealthInsurance();
            };

        }

        public async void CheckbtnBHYT()
        {
            funcBHYT = (await funcBHYT.GetData()).ListT[0];
            DateTime date = DateTime.Now;
            if (date < funcBHYT.StartDate || date > funcBHYT.EndDate)
            {
                btnHealthInsurance.IsVisible = false;
                StackLayoutMain.Children.Remove(pkrDKBHYT);
                bhyt = false;
            }
            else
            {
                bhyt = true;
                pkrDKBHYT.ItemsSource = new string[]
                {
                    "Đăng ký bảo hiểm y tế.",
                    "Đã có bảo hiểm quân đội của cha mẹ."
                };
            }
            
        }

        public void EditNameBtnHealthInsurance()
        {
            if (!bhyt)
            {
                return;
            }
            else
            {
                if (App.StudentSeclect.BHQD == 1)
                {
                    btnHealthInsurance.Text = "Hủy BH Quân đội";
                }
                else
                {
                    if (App.StudentSeclect.DangKy == 1)
                    {
                        btnHealthInsurance.Text = "Hủy BHYT";
                    }
                    else
                    {
                        btnHealthInsurance.Text = "Đăng ký BHYT";
                    }
                }
            }
        }

        #region Event Button

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

        private async void BtnSchedule_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimeTablePage());
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

        private async void BtnPoint_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PointPage());
        }

        private async void BtnSummary_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SummaryPage());
        }

        private async void BtnHealthInsurance_OnClicked(object sender, EventArgs e)
        {
            if (btnHealthInsurance.Text.Equals("Hủy BHYT"))
            {
                await DisplayAlert("Thông báo", "Huy BHYT", "OK");
            }
            else if (btnHealthInsurance.Text.Equals("Hủy BH Quân đội"))
            {
                await DisplayAlert("Thông báo", "Huy BHQD", "OK");

            }
            else
            {
                pkrDKBHYT.Focus();
            }
        }

        #endregion
    }
}