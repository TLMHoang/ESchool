using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;
using MobileSoLienLac.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Notify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NClassPage : ContentPage
    {
        public ThongBaoLop VaLop = new ThongBaoLop();

        public NClassPage()
        {
            InitializeComponent();
            NotifyClass();
            if (App.LstThongBaoLops.Count != 0)
            {
                List<NotifyViewModel> lst = new List<NotifyViewModel>();
                foreach (var i in App.LstThongBaoLops)
                {
                    lst.Add(new NotifyViewModel(i.ID, i.TenThongBao, i.Ngay));
                }

                lst = lst.OrderByDescending(p => p.ID).ToList();
                lvLstNotify.ItemsSource = lst;
                StackLayoutNS.Children.Remove(lblNotify);
                lvLstNotify.ItemSelected += async (sender, e) =>
                {
                    if (lvLstNotify.SelectedItem != null)
                    {
                        DataTable dt = await VaLop.GetContent((int)((NotifyViewModel)e.SelectedItem).ID);
                        if (dt.Columns.Count == 1)
                        {
                            await DisplayAlert("Thông báo",
                                new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                        }
                        else
                        {
                            ThongBaoLop tbt = VaLop.GetData(dt.Rows[0]);
                            await DisplayAlert((string)((NotifyViewModel)e.SelectedItem).TenThongBao, tbt.NoiDung, "OK");
                        }
                    }
                    lvLstNotify.SelectionMode = ListViewSelectionMode.None;
                    lvLstNotify.SelectionMode = ListViewSelectionMode.Single;
                };
            }
            else
            {
                StackLayoutNS.Children.Remove(lvLstNotify);
            }
        }

        public async void NotifyClass()
        {
            string Message = await new NotifyViewModel().NotifyClass();
            if (Message != "")
            {
                lblNotify.Text = "Vui lòng thử lại.";
                await DisplayAlert("Thông báo", Message, "OK");
            }
        }
    }
}