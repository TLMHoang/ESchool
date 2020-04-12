using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;
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
        List<NotifyModel> lstNS = new List<NotifyModel>();

        public NSchoolPage()
        {
            InitializeComponent();
            NotifySchool();
            if (lstNS.Count != 0)
            {
                List<NotifyViewModel> lst = new List<NotifyViewModel>();
                foreach (var i in lstNS)
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
                        ValueDTO<ThongBaoTruong> val = await valThongBaoTruong.GetContent((int) ((NotifyViewModel) e.SelectedItem).ID);
                        if (val.Error != 1)
                        {
                            await DisplayAlert("Thông báo",
                                new HandleError().IDErrorToNotify(val.Error), "OK");
                        }
                        else
                        {
                            if (val.ListT[0].NoiDung.Length >= 1000)
                            {
                                await Navigation.PushAsync(new ShowNewPage((string)((NotifyViewModel)e.SelectedItem).TenThongBao, val.ListT[0].Ngay.ToString("dd-MM-yyyy"), val.ListT[0].NoiDung));
                            }
                            else
                            {
                                await DisplayAlert((string)((NotifyViewModel)e.SelectedItem).TenThongBao, val.ListT[0].NoiDung, "OK");
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
            ValueDTO<NotifyModel> val = await new NotifyViewModel().NotifySchool();
            if (val.Error != 0)
            {
                lblNotify.Text = "Vui lòng thử lại.";
                await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(val.Error), "OK");
            }
            else
            {
                lstNS = val.ListT;
            }
        }
    }
}