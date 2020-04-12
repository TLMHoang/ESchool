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


        public ListFeeByMonth()
        {
            InitializeComponent();

            Title = "Học phí - " + App.StudentSeclect.Ten;

            for (int i = 1; i < 13; i++)
            {
                PickerMonth.Items.Add("Tháng " + i);
            }


            PickerMonth.SelectedIndexChanged += async (sender, e) =>
            {
                ValueDTO<HocPhi> val = await GetData(GetMonth(PickerMonth.Items[PickerMonth.SelectedIndex]));
                if (val.Error == 0)
                {
                    if (val.ListT.Count == 0)
                    {
                        FillData();
                    }
                    else
                    {
                        FillData(val.ListT[0]);
                    }
                }
            };
        }

        public async Task<ValueDTO<HocPhi>> GetData(int thang)
        {
            ObjectSQL<int> NgayNghi = await new DiemDanh().GetAbsent(App.StudentSeclect.ID, thang, Convert.ToByte(1));
            ValueDTO<HocPhi> val = new ValueDTO<HocPhi>();
            if (NgayNghi.Error == 0)
            {
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
            lblDien.Text = "0";
            lblHoc.Text = "0";
            lblNuoc.Text = "0";
            lblTaiLieu.Text = "0";
            lblTTBi.Text = "0";
            lblVeSinh.Text = "0";
            lblTong.Text = "0";
        }

        public void FillData(HocPhi hp)
        {
            lblAn.Text = FormatIntToString(hp.TienAn);
            lblDien.Text = FormatIntToString(hp.TienDien);
            lblHoc.Text = FormatIntToString(hp.TienHoc);
            lblNuoc.Text = FormatIntToString(hp.TienNuoc);
            lblTaiLieu.Text = FormatIntToString(hp.TienTaiLieu);
            lblTTBi.Text = FormatIntToString(hp.TienTrangThietBi);
            lblVeSinh.Text = FormatIntToString(hp.TienVeSinh);
            lblTong.Text = FormatIntToString(hp.TongTien);
        }

        public string FormatIntToString(int value) => (value * 1000).ToString("N0");
    }
}