using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Javax.Xml.Transform;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student.RollCall
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationRollCallPage : ContentPage
    {
        public XinPhep xp = new XinPhep();
        private bool Save = true;
        private DateTime date = DateTime.Now;
        public RegistrationRollCallPage()
        {
            InitializeComponent();
            
            dpkTo.Date = dpkFrom.Date = new DateTime(date.Year, date.Month, date.Day + 3);
            date = dpkFrom.Date;
            #region Giới hạn set ngày

            dpkFrom.DateSelected += (sender, e) =>
            {
                if (dpkFrom.Date < date)
                {
                    dpkFrom.Date = date;
                    dpkTo.Date = dpkFrom.Date;
                    DisplayAlert("Thông báo",
                        "Cần xin phép trước 3 ngày.\nNếu trường hợp có thể gọi trực tiếp cho giao viên chủ nhiệm",
                        "OK");
                }
            };
            dpkTo.DateSelected += (sender, e) =>
            {
                if (dpkTo.Date < dpkFrom.Date)
                {
                    dpkTo.Date = dpkFrom.Date;
                    DisplayAlert("Thông báo",
                        "Ngày nghỉ cuối cùng không thể bằng ngày nghỉ đầu tiên",
                        "OK");
                }
            };

            #endregion
        }

        public RegistrationRollCallPage(XinPhep val, bool save)
        {
            InitializeComponent();
            Save = save;
            xp = val;
            FillData(val);
            ViewPage(save);
            if (!save)
            {
                if (val.NghiDen < date)
                {
                    btnEdit.Text = "";
                    btnEdit.IsEnabled = save;
                }
            }

            #region Giới hạn set ngày

            dpkFrom.DateSelected += (sender, e) =>
            {
                if (dpkFrom.Date < date)
                {
                    dpkFrom.Date = date;
                    dpkTo.Date = dpkFrom.Date;
                    DisplayAlert("Thông báo",
                        "Cần xin phép trước 3 ngày.\nNếu trường hợp có thể gọi trực tiếp cho giao viên chủ nhiệm",
                        "OK");
                }
            };
            dpkTo.DateSelected += (sender, e) =>
            {
                if (dpkTo.Date < dpkFrom.Date)
                {
                    dpkTo.Date = dpkFrom.Date;
                    DisplayAlert("Thông báo",
                        "Ngày nghỉ cuối cùng không thể bằng ngày nghỉ đầu tiên",
                        "OK");
                }
            };

            #endregion
        }

        public void FillData(XinPhep val)
        {
            dpkFrom.Date = val.NghiTu;
            dpkTo.Date = val.NghiDen;
            edtContent.Text = val.LyDo;
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (edtContent.Text == null || edtContent.Text == "")
            {
                await DisplayAlert("Thông báo",
                    "Nhập lý do để giáo viên chủ nhiệm có thể duyệt.",
                    "OK");
                edtContent.Focus();
                return;
            }

            if (Save)
            {
                int val = await xp.SetData(App.StudentSeclect.ID, dpkFrom.Date, dpkTo.Date, edtContent.Text);
                if (val == 0)
                {
                    await DisplayAlert("Thông báo",
                        "Có lỗi xảy ra vui lòng thử lại",
                        "OK");
                }
                else if (val < 0)
                {
                    await DisplayAlert("Thông báo",
                        new HandleError().IDErrorToNotify(val),
                        "OK");
                }
                else
                {
                    await DisplayAlert("Thông báo",
                        "Đã gửi cho GVCN, vui lòng đợi GVCN duyệt.",
                        "OK");
                }
            }
            else
            {
                int IntReturn = await xp.UpdateData(xp.ID, dpkFrom.Date, dpkTo.Date, edtContent.Text);
                if (IntReturn == 0)
                {
                    await DisplayAlert("Thông báo",
                        "Có lỗi xảy ra vui lòng thử lại",
                        "OK");
                }
                else if (IntReturn < 0)
                {
                    await DisplayAlert("Thông báo",
                        new HandleError().IDErrorToNotify(IntReturn),
                        "OK");
                }
                else
                {
                    await DisplayAlert("Thông báo",
                        "Đã gửi cập nhập cho GVCN, vui lòng đợi GVCN duyệt.",
                        "OK");
                }
            }
        }


        private void BtnEdit_OnClicked(object sender, EventArgs e)
        {
            if (btnEdit.Text.Equals("Sửa"))
            {
                btnEdit.Text = "Xem";
                if (dpkFrom.Date < date)
                {
                    ViewPage(true);
                    dpkTo.IsEnabled = false;
                }
                else
                {
                    ViewPage(true);
                }
            }
            else
            {
                btnEdit.Text = "Sửa";
                ViewPage(false);
            }
        }

        private void ViewPage(bool val)
        {
            dpkFrom.IsEnabled = dpkTo.IsEnabled = edtContent.IsEnabled = val;
            btnSave.IsVisible = val;
        }
    }
}