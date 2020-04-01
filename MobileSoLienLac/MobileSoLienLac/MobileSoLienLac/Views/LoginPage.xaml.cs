using Android.Widget;
using MobileSoLienLac.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Support.V4.App;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;
using MobileSoLienLac.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Khoi valKhoi = new Khoi();
        public Lop valLop;
        public HandleError error;
        public Login()
        {
            InitializeComponent();
            //img_Logo.Source = ImageSource.FromFile("sourcelogo.png");
        }

        public async Task LoadClass()
        {
            DataTable dtk = new DataTable();
            dtk = await valKhoi.GetData();
            if (dtk.Columns.Count == 1)
            {
                Toast.MakeText(Android.App.Application.Context, "Mạng yếu vui lòng đợi", ToastLength.Long).Show();
                dtk = new DataTable();
                await valKhoi.GetData();
                if (dtk.Columns.Count == 1)
                {
                    await DisplayAlert("Thông báo",error.IDErrorToNotify(Convert.ToInt32(dtk.Rows[0]["Error"])) , "OK");
                    btn_Login.IsEnabled = true;
                    return;
                }
            }
            App.lstLops = await new Lop().GetList();
        }

        private async void btn_Login_Clicked(object sender, EventArgs e)
        {
            btn_Login.IsEnabled = false;
            int val = new ModelLogin().CheckEntryNull(Entry_Username.Text, Entry_Password.Text);
            if (val == 0)
            {
                string strRetrun = (await new LoginViewModel().CheckLogin(Entry_Username.Text, Entry_Password.Text));
                if (strRetrun.Length == 0)
                {
                    await LoadClass();
                    await DisplayAlert("Thông báo", "Đăng nhập thành công", "Ok");

                    //while (App.lstKhois.Count == 0 || App.lstLops.Count == 0)
                    //{
                    //    if (expr)
                    //    {

                    //    }
                    //}
                    App.StudentSeclect = App.lstStudents.FirstOrDefault();

                    Application.Current.MainPage = new MainPage();
                }
                else if (strRetrun.Length == 0)
                {
                    await DisplayAlert("Thông báo", "Đăng nhập thất bại", "OK");
                    LoginFaild();
                    btn_Login.IsEnabled = true;
                }
                else
                {
                    await DisplayAlert("Thông báo", strRetrun, "OK");
                    btn_Login.IsEnabled = true;
                }
            }
            else
            {
                switch (val)
                {
                    case 1:
                        {
                            await DisplayAlert("Thông báo", "Vui Lòng nhập TÀI KHOẢN", "OK");
                            Entry_Username.Focus();
                            break;
                        }
                    case 2:
                        {
                            await DisplayAlert("Thông báo", "Vui Lòng nhập MẬT KHẨU", "OK");
                            Entry_Password.Focus();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                btn_Login.IsEnabled = true;
            }
        }

        public void LoginFaild()
        {
            Entry_Username.Text = null;
            Entry_Password.Text = null;
            Entry_Username.Focus();
        }

        private async void btn_Close_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Thông báo", "Bạn có muốn thoát ứng dụng ?", "YES", "NO"))
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
            
        }

        private void Entry_Username_OnCompleted(object sender, EventArgs e)
        {
            Entry_Password.Focus();
        }

        private void Entry_Password_OnCompleted(object sender, EventArgs e)
        {
            btn_Login_Clicked(null,null);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Image img = (Image) sender;
            if (Entry_Password.IsPassword)
            {
                img.Source = ImageSource.FromFile("show.png");
                Entry_Password.IsPassword = false;
            }
            else
            {
                img.Source = ImageSource.FromFile("hiden.png");
                Entry_Password.IsPassword = true;
            }
        }
    }
}