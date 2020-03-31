using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Switch = Xamarin.Forms.Switch;

namespace MobileSoLienLac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePass : ContentPage
    {
        public ChangePass()
        {
            InitializeComponent();

            Entry_OldPass.Completed += (sender, e) => { Entry_NewPass.Focus(); };
            Entry_NewPass.Completed += (sender, e) => { Entry_ComplePass.Focus(); };
        }


        private async void btnChange_Clicked(object sender, EventArgs e)
        {
            if (Check())
            {
                if (await new TaiKhoan().ChangePassword(App.IDAccount, Entry_OldPass.Text, Entry_ComplePass.Text) != -1)
                {
                    DisplayAlert("Thông báo", "Đổi mật khẩu thành công.", "OK");
                }
                else
                {
                    DisplayAlert("Thông báo", "Đổi mật khẩu thât bại.", "OK");
                }
            }
        }

        private bool Check()
        {
            if (Entry_OldPass.Text == null || Entry_OldPass.Text.Length == 0)
            {
                Toast.MakeText(Android.App.Application.Context, "Chưa nhập\nMật khẩu hiện tại", ToastLength.Long).Show();
                Entry_OldPass.Focus();
                return false;
            }
            if (Entry_NewPass.Text == null || Entry_NewPass.Text.Length == 0)
            {
                Toast.MakeText(Android.App.Application.Context, "Chưa nhập\nMật khẩu mới", ToastLength.Long).Show();
                Entry_NewPass.Focus();
                return false;
            }
            if (Entry_ComplePass.Text == null || Entry_ComplePass.Text.Length == 0)
            {
                Toast.MakeText(Android.App.Application.Context, "Chưa nhập\nNhập lại mật khẩu mới", ToastLength.Long).Show();
                Entry_ComplePass.Focus();
                return false;
            }
            if (!Entry_ComplePass.Text.Equals(Entry_NewPass.Text))
            {
                Toast.MakeText(Android.App.Application.Context, "Nhập lại mật khẩu mới không chính xác", ToastLength.Long).Show();
                Entry_ComplePass.Focus();
                return false;
            }
            if (Entry_ComplePass.Text.Equals(Entry_OldPass.Text))
            {
                Toast.MakeText(Android.App.Application.Context, "Mật khẩu mới trùng mật khẩu hiện tại", ToastLength.Long).Show();
                Entry_NewPass.Focus();
                return false;
            }
            return true;
        }

        private void SwitchShowHiden_OnToggled(object sender, ToggledEventArgs e)
        {
            if (((Switch)sender).IsToggled)
            {
                Entry_ComplePass.IsPassword = Entry_NewPass.IsPassword = Entry_OldPass.IsPassword = false;
            }
            else
            {
                Entry_ComplePass.IsPassword = Entry_NewPass.IsPassword = Entry_OldPass.IsPassword = true;
            }
        }

        private void Entry_OldPass_OnTapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            if (Entry_OldPass.IsPassword)
            {
                img.Source = ImageSource.FromFile("show.png");
                Entry_OldPass.IsPassword = false;
            }
            else
            {
                img.Source = ImageSource.FromFile("hiden.png");
                Entry_OldPass.IsPassword = true;
            }
        }

        private void Entry_NewPass_OnTapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            if (Entry_NewPass.IsPassword)
            {
                img.Source = ImageSource.FromFile("show.png");
                Entry_NewPass.IsPassword = false;
            }
            else
            {
                img.Source = ImageSource.FromFile("hiden.png");
                Entry_NewPass.IsPassword = true;
            }
        }

        private void Entry_ComplePass_OnTapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            if (Entry_ComplePass.IsPassword)
            {
                img.Source = ImageSource.FromFile("show.png");
                Entry_ComplePass.IsPassword = false;
            }
            else
            {
                img.Source = ImageSource.FromFile("hiden.png");
                Entry_ComplePass.IsPassword = true;
            }
        }
    }
}