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
        
        public ThongTinHS valHS = new ThongTinHS();
        public HandleError error = new HandleError();
        public Login()
        {
            InitializeComponent();
        }

        public async Task<bool> LoadGrade()
        {
            Khoi val = new Khoi();
            DataTable dt = await val.GetData();
            if (dt.Columns.Count == 1)
            {
                await DisplayAlert("Thông báo", error.IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                btn_Login.IsEnabled = true;
                App.ResetSource();
                return false;
            }
            else
            {
                App.lstKhois = val.GetData(dt);
                return true;
            }
        }

        public async Task<bool> LoadClass()
        {
            Lop val = new Lop();
            DataTable dt = await val.GetData();
            if (dt.Columns.Count == 1)
            {
                await DisplayAlert("Thông báo", error.IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                btn_Login.IsEnabled = true;
                App.ResetSource();
                return false;
            }
            else
            {
                App.lstLops = val.GetData(dt);
                return true;
            }
        }
        //dang bug
        public async Task<bool> LoadLink()
        {
            LienKetPHvsHS val = new LienKetPHvsHS();
            DataTable dt = await val.GetData(App.IDAccount);
            if (dt.Columns.Count == 1)
            {
                int IDError = Convert.ToInt32(dt.Rows[0]["Error"]);
                string mess = error.IDErrorToNotify(IDError);
                await DisplayAlert("Thông báo", mess, "OK");
                btn_Login.IsEnabled = true;
                App.ResetSource();
                return false;
            }
            else
            {
                App.lstPHvsHs = val.GetData(dt);
                return true;
            }
        }

        public async Task<bool> LoadConduct()
        {
            LoaiHanhKiem val = new LoaiHanhKiem();
            DataTable dt = await val.GetData();
            if (dt.Columns.Count == 1)
            {
                await DisplayAlert("Thông báo", error.IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                btn_Login.IsEnabled = true;
                App.ResetSource();
                return false;
            }
            else
            {
                App.lstLoaiHanhKiems = val.GetData(dt);
                return true;
            }
        }

        public async Task<bool> LoadSpeciesStudent()
        {
            LoaiHocSinh val = new LoaiHocSinh();
            DataTable dt = await val.GetData();
            if (dt.Columns.Count == 1)
            {
                await DisplayAlert("Thông báo", error.IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                btn_Login.IsEnabled = true;
                App.ResetSource();
                return false;
            }
            else
            {
                App.lstLoaiHocSinhs = val.GetData(dt);
                return true;
            }
        }

        public async Task<bool> LoadSpeciesPoint()
        {
            LoaiDiem val = new LoaiDiem();
            DataTable dt = await val.GetData();
            if (dt.Columns.Count == 1)
            {
                await DisplayAlert("Thông báo", error.IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                btn_Login.IsEnabled = true;
                App.ResetSource();
                return false;
            }
            else
            {
                App.lstLoaiDiems = val.GetData(dt);
                return true;
            }
        }

        public async Task<bool> LoadDataInDatabase()
        {
            if (!(await LoadGrade())) return false;
            if (!(await LoadClass())) return false;
            if (!(await LoadLink())) return false;
            if (!(await LoadConduct())) return false;
            if (!(await LoadSpeciesStudent())) return false;
            if (!(await LoadSpeciesPoint())) return false;

            foreach (LienKetPHvsHS i in App.lstPHvsHs)
            {
                #region Load Strudent 

                DataTable dt = await valHS.GetDaTa(i.IDHocSinh);
                if (dt.Columns.Count == 1)
                {
                    
                    await DisplayAlert("Thông báo", error.IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                    btn_Login.IsEnabled = true;
                    App.ResetSource();
                    return false;
                }
                else
                {
                    App.lstStudents.Add(new ThongTinHS(dt.Rows[0]));
                }

                #endregion
            }

            App.StudentSeclect = App.lstStudents.FirstOrDefault();

            return true;
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
                    if (await LoadDataInDatabase())
                    {
                        await DisplayAlert("Thông báo", "Đăng nhập thành công", "Ok");

                        Application.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        await DisplayAlert("Thông báo", "Đăng nhập thất bại. Không thể load được vì mạng yếu. hoặc chưa bật mạng vui lòng kiểm tra lại.", "OK");
                        LoginFaild();
                        btn_Login.IsEnabled = true;
                    }
                    
                }
                else if (strRetrun.Length == 1)
                {
                    await DisplayAlert("Thông báo", "Đăng nhập thất bại.\nTài khoản hoặc mật khẩu không chính xác", "OK");
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