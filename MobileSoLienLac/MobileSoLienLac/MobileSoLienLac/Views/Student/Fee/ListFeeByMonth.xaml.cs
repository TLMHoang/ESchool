using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student.Fee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListFeeByMonth : ContentPage
    {
        private int NghiP;

        public ListFeeByMonth()
        {
            InitializeComponent();
            //FillData();

            Title = "Học phí - " + App.StudentSeclect.Ten;

            for (int i = 1; i < 13; i++)
            {
                PickerMonth.Items.Add("Tháng " + i);
            }

            PickerMonth.SelectedIndex = DateTime.Now.Month - 1;
            GetValue(DateTime.Now.Month);
            PickerMonth.SelectedIndexChanged += (sender, e) =>
            {
                GetValue(GetMonth(PickerMonth.Items[PickerMonth.SelectedIndex]));
            };
        }

        public async void GetValue(int month)
        {
            ValueDTO<HocPhi> val = await GetData(month);
            if (val.Error == 0)
            {
                if (val.ListT.Count == 0)
                {
                    FillData();
                }
                else
                {
                    FillData(val.ListT[0], month);
                }
            }
        }

        public async Task<ValueDTO<HocPhi>> GetData(int thang)
        {
            ObjectSQL<int> NgayNghi = await new DiemDanh().GetAbsent(App.StudentSeclect.ID, thang, Convert.ToByte(1));
            ValueDTO<HocPhi> val = new ValueDTO<HocPhi>();
            if (NgayNghi.Error == 0)
            {
                NghiP = NgayNghi.Value;
                val = await new HocPhi().GetData(thang, App.StudentSeclect.IDLoaiHocSinh, App.StudentSeclect.IDKhoi, NgayNghi.Value);
                if (val.Error != 0)
                {
                    await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(val.Error), "OK");
                    FillData();
                }
            }
            else
            {
                await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(NgayNghi.Error), "OK");
                FillData();
            }

            return val;
        }

        public int GetMonth(string strMonth)
        {
            string str = strMonth.Replace("Tháng ", "");
            return Convert.ToInt32(str);
        }

        public void FillData()
        {
            lblAn.Text = "0";
            lblDonGia.Text = "0";
            lblNgayH.Text = "0";
            lblNgayN.Text = "0";
            lblDien.Text = "0";
            lblHoc.Text = "0";
            lblNuoc.Text = "0";
            lblTaiLieu.Text = "0";
            lblTTBi.Text = "0";
            lblVeSinh.Text = "0";
            lblTong.Text = "0";
            lblTien.Text = "0";
        }

        public void FillData(HocPhi hp, int month)
        {
            lblAn.Text = lblDonGia.Text = lblNgayH.Text = lblNgayN.Text = lblDien.Text = lblHoc.Text = lblNuoc.Text = lblTaiLieu.Text = lblTTBi.Text = lblVeSinh.Text = lblTong.Text = lblTien.Text = "";
            if (month == DateTime.Now.Month)
            {
                lblAn.Text = FormatIntToString(hp.TongAn);
                lblDonGia.Text = FormatIntToString(hp.TienAn);
                lblNgayH.Text = hp.SoNgayHoc + " ngày";
                lblNgayN.Text = 0 + " ngày";
                lblDien.Text = FormatIntToString(hp.TienDien);
                lblHoc.Text = FormatIntToString(hp.TienHoc);
                lblNuoc.Text = FormatIntToString(hp.TienNuoc);
                lblTaiLieu.Text = FormatIntToString(hp.TienTaiLieu);
                lblTTBi.Text = FormatIntToString(hp.TienTrangThietBi);
                lblVeSinh.Text = FormatIntToString(hp.TienVeSinh);
                lblTong.Text = FormatIntToString(hp.TongTien - App.StudentSeclect.Tien);
                lblTien.Text = FormatIntToString(App.StudentSeclect.Tien);
                sloTien.IsVisible = true;
            }
            else
            {
                lblAn.Text = FormatIntToString(hp.TongAn - (hp.TienAn * NghiP));
                lblDonGia.Text = FormatIntToString(hp.TienAn);
                lblNgayH.Text = hp.SoNgayHoc + " ngày";
                lblNgayN.Text = NghiP + " ngày";
                lblDien.Text = FormatIntToString(hp.TienDien);
                lblHoc.Text = FormatIntToString(hp.TienHoc);
                lblNuoc.Text = FormatIntToString(hp.TienNuoc);
                lblTaiLieu.Text = FormatIntToString(hp.TienTaiLieu);
                lblTTBi.Text = FormatIntToString(hp.TienTrangThietBi);
                lblVeSinh.Text = FormatIntToString(hp.TienVeSinh);
                lblTong.Text = FormatIntToString(hp.TongTien - (hp.TienAn * NghiP));
                sloTien.IsVisible = false;
            }
        }

        public string FormatIntToString(int value)
        {
            if (value == 0)
            {
                return "0";
            }
            string str = (value * 1000).ToString("N0");
            return str;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            sloCTAn.IsVisible = !sloCTAn.IsVisible;
        }
    }
}