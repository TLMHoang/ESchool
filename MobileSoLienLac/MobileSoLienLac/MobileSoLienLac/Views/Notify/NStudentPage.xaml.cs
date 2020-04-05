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
    public partial class NStudentPage : ContentPage
    {
        public ThongBaoHS ValHs = new ThongBaoHS();

        public NStudentPage()
        {
            InitializeComponent();
            NotifyStudent();
            if (App.LstThongBaoHSs.Count != 0)
            {
                List<NotifyViewModel> lst = new List<NotifyViewModel>();
                foreach (var i in App.LstThongBaoHSs)
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
                        DataTable dt = await ValHs.GetContent((int)((NotifyViewModel)e.SelectedItem).ID);
                        if (dt.Columns.Count == 1)
                        {
                            await DisplayAlert("Thông báo",
                                new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                        }
                        else
                        {
                            ThongBaoHS tbt = ValHs.GetData(dt.Rows[0]);
                            DisplayAlert((string)((NotifyViewModel)e.SelectedItem).TenThongBao, tbt.NoiDung, "OK");

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

        public async void NotifyStudent()
        {
            string Message = await new NotifyViewModel().NotifyStudent();
            if (Message != "")
            {
                lblNotify.Text = "Vui lòng thử lại.";
                await DisplayAlert("Thông báo", Message, "OK");
            }
        }
    }
}