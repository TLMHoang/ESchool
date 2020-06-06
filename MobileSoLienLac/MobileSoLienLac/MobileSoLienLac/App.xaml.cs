using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileSoLienLac.Services;
using MobileSoLienLac.Views;
using MobileSoLienLac.Views.Student;
using System.Security.Permissions;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Java.Util;
using MobileSoLienLac.Views.Notify;

namespace MobileSoLienLac
{
    public partial class App : Application
    {
        #region Source for app get in database

        public static List<LienKetPHvsHS> lstPHvsHs;
        public static List<ThongTinHS> lstStudents = new List<ThongTinHS>();
        public static ThongTinHS StudentSeclect;
        public static List<LoaiDiem> lstLoaiDiems = new List<LoaiDiem>();
        public static List<LoaiHanhKiem> lstLoaiHanhKiems = new List<LoaiHanhKiem>();
        public static List<MonHoc> lstMonHocs = new List<MonHoc>();

        //public delegate void LoadNotify();
        //public event LoadNotify OnLoadNotify;

        public static int IDAccount;
        public static string UserName = "";
        #endregion
        public App()
        {
            InitializeComponent();
            
            Dictionary<string,object> val = new Dictionary<string, object>();



            DependencyService.Register<MockDataStore>();
            MainPage = new Login();

            OneSignal.Current.StartInit("24bb2a3a-7946-4823-aad5-5dc026481279")
                .Settings(new Dictionary<string, bool>() {
                    { IOSSettings.kOSSettingsKeyAutoPrompt, false },
                    { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
                .EndInit();

            
        }

        public static void ResetSource()
        {
            lstPHvsHs = new List<LienKetPHvsHS>();
            lstStudents = new List<ThongTinHS>();
            StudentSeclect = new ThongTinHS();
            lstLoaiDiems = new List<LoaiDiem>();
            lstLoaiHanhKiems = new List<LoaiHanhKiem>();
            lstMonHocs = new List<MonHoc>();

            IDAccount = -1;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
