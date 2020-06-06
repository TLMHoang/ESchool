using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileSoLienLac.Models;
using Android.Widget;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Views.Student;
using MobileSoLienLac.Views.Student.Fee;

//using TableDependency.SqlClient;
//using TableDependency.SqlClient.Base.EventArgs;
//using TableDependency.SqlClient.Base.Enums;

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

            OneSignal.Current.SendTag("id_user_PHHS",App.UserName);

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
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
                    case (int)MenuItemType.Point:
                        MenuPages.Add(id, new NavigationPage(new PointPage()));
                        break;
                    case (int)MenuItemType.Fee:
                        MenuPages.Add(id, new NavigationPage(new ListFeeByMonth()));
                        break;
                    case (int)MenuItemType.TTable:
                        MenuPages.Add(id, new NavigationPage(new TimeTablePage()));
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