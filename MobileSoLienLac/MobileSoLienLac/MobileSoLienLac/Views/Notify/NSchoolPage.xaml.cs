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
    public partial class NSchoolPage : ContentPage
    {
        ThongBaoTruong valThongBaoTruong = new ThongBaoTruong();

        public NSchoolPage()
        {
            InitializeComponent();
            NotifySchool();
            if (App.LstThongBaoTruongs.Count != 0)
            {
                List<NotifyViewModel> lst = new List<NotifyViewModel>();
                foreach (var i in App.LstThongBaoTruongs)
                {
                    lst.Add(new NotifyViewModel(i.ID,i.TenThongBao,i.Ngay));
                }

                lst = lst.OrderByDescending(p => p.ID).ToList();
                lvLstNotify.ItemsSource = lst;
                StackLayoutNS.Children.Remove(lblNotify);
                lvLstNotify.ItemSelected += async (sender, e) =>
                {

                    if (lvLstNotify.SelectedItem != null)
                    {
                        DataTable dt = await valThongBaoTruong.GetContent((int) ((NotifyViewModel) e.SelectedItem).ID);
                        if (dt.Columns.Count == 1)
                        {
                            await DisplayAlert("Thông báo",
                                new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"])), "OK");
                        }
                        else
                        {
                            ThongBaoTruong tbt = valThongBaoTruong.GetData(dt.Rows[0]);
                            if (tbt.NoiDung.Length >= 1000)
                            {
                                await Navigation.PushAsync(new ShowNewPage((string)((NotifyViewModel)e.SelectedItem).TenThongBao, tbt.Ngay.ToString("dd-MM-yyyy"), tbt.NoiDung));
                            }
                            else
                            {
                                await DisplayAlert((string)((NotifyViewModel)e.SelectedItem).TenThongBao, tbt.NoiDung, "OK");
                            }


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

        public async void NotifySchool()
        {
            string Message = await new NotifyViewModel().NotifySchool();
            if (Message != "")
            {
                lblNotify.Text = "Vui lòng thử lại.";
                await DisplayAlert("Thông báo", Message, "OK");
            }
        }
    }
}