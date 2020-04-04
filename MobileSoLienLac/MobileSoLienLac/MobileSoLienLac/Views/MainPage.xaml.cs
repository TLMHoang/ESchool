using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileSoLienLac.Models;
using Android.Widget;
using MobileSoLienLac.DTO;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient.Base.Enums;

namespace MobileSoLienLac.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public void WaitNotify()
        {
            string connStr = @"SERVER=125.212.218.20; Initial Catalog=nxtckedu_HeThongSoLienLac; Integrated Security=false; uid= nxtckedu_sa; pwd= H*P*T-1999";
            using (var result = new SqlTableDependency<LoaiHocSinh>(connStr, "Function"))
            {
                result.OnChanged += Result_OnChanged;
                result.OnError += Result_OnError;

                result.Start();
            }
        }

        private void Result_OnError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Result_OnChanged(object sender, RecordChangedEventArgs<LoaiHocSinh> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                var Value = e.Entity;
                string val = e.ChangeType.ToString() + "\n";
                DisplayAlert("Thông báo", val + Value.TenLoai, "OK");
            }
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.Notify:
                        MenuPages.Add(id, new NavigationPage(new NotifyPage()));
                        break;
                    case (int)MenuItemType.ChangePass:
                        MenuPages.Add(id, new NavigationPage(new ChangePass()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        App.ResetSource();
                        Application.Current.MainPage = new Login();
                        return;

                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}