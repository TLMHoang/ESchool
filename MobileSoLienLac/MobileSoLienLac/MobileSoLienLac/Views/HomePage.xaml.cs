using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
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


            pkrDKBHYT.SelectedIndexChanged += async (sender, e) =>
            {
                if (pkrDKBHYT.SelectedIndex == 0)
                {
                    int result = await viewModel.EditBHYT(App.StudentSeclect.ID, 1, 1);
                    if (result == 1)
                    {
                        await DisplayAlert("Thông báo", "Đăng ký BHYT thành công", "OK");
                        App.StudentSeclect.DangKy = 1;
                        EditNameBtnHealthInsurance();
                    }
                    else
                    {
                        await DisplayAlert("Thông báo", "Đăng ký BHYT thất bại", "OK");
                    }
                }
                else if (pkrDKBHYT.SelectedIndex == 1)
                {
                    int result = await viewModel.EditBHYT(App.StudentSeclect.ID, 0, 1);
                    if (result == 1)
                    {
                        await DisplayAlert("Thông báo", "Xác nhận có BHQD thành công", "OK");
                        App.StudentSeclect.BHQD = 1;
                        EditNameBtnHealthInsurance();
                    }
                    else
                    {
                        await DisplayAlert("Thông báo", "Xác nhận có BHQD thất bại", "OK");
                    }
                }


                pkrDKBHYT.SelectedIndex = -1;
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
                int result = await viewModel.EditBHYT(App.StudentSeclect.ID, 1, 0);
                if (result == 1)
                {
                    await DisplayAlert("Thông báo", "Hủy BHYT thành công", "OK");
                        App.StudentSeclect.DangKy = 0;
                        EditNameBtnHealthInsurance();
                }
                else
                {
                    await DisplayAlert("Thông báo", "Hủy BHYT thất bại", "OK");
                }
            }
            else if (btnHealthInsurance.Text.Equals("Hủy BH Quân đội"))
            {
                int result = await viewModel.EditBHYT(App.StudentSeclect.ID, 1, 0);
                if (result == 1)
                {
                    await DisplayAlert("Thông báo", "Hủy BHQD thành công", "OK");
                        App.StudentSeclect.BHQD = 0;
                        EditNameBtnHealthInsurance();
                }
                else
                {
                    await DisplayAlert("Thông báo", "Hủy BHQD thất bại", "OK");
                }

            }
            else
            {
                pkrDKBHYT.Focus();
            }
        }

        #endregion

        //Post notify to Teacher
        //private void BtnTestNotify_OnClicked(object sender, EventArgs e)
        //{
        //    var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

        //    request.KeepAlive = true;
        //    request.Method = "POST";
        //    request.ContentType = "application/json; charset=utf-8";

        //    request.Headers.Add("authorization", "Basic ODliOGM5OTYtY2E0Yi00NzVkLTk5MzktNTc5ZTE4YzM2MGIy");

        //    byte[] byteArray = Encoding.UTF8.GetBytes("{"
        //                                              + "\"app_id\": \"24bb2a3a-7946-4823-aad5-5dc026481279\","
        //                                              + "\"contents\": {\"en\": \"English Message\"},"
        //                                              + "\"filters\": [{\"field\": \"tag\", \"key\": \"id_user_PHHS\", \"relation\": \"=\", \"value\": \"phong\"}]}");

        //    string responseContent = null;

        //    try
        //    {
        //        using (var writer = request.GetRequestStream())
        //        {
        //            writer.Write(byteArray, 0, byteArray.Length);
        //        }

        //        using (var response = request.GetResponse() as HttpWebResponse)
        //        {
        //            using (var reader = new StreamReader(response.GetResponseStream()))
        //            {
        //                responseContent = reader.ReadToEnd();
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //        System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
        //    }

        //    System.Diagnostics.Debug.WriteLine(responseContent);
        //}
    }
}