using System;
using System.Collections.Generic;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileSoLienLac.Services;
using MobileSoLienLac.Views;

namespace MobileSoLienLac
{
    public partial class App : Application
    {
        #region Source for app get in database

        public static List<LienKetPHvsHS> lstPHvsHs;
        public static List<ThongTinHS> lstStudents = new List<ThongTinHS>();
        public static ThongTinHS StudentSeclect;
        public static List<Lop> lstLops;
        public static List<Khoi> lstKhois ;
        public static int IDAccount;
        public static bool DakMode;
        #endregion
        public App()
        {
            InitializeComponent();

            

            DependencyService.Register<MockDataStore>();
            MainPage = new Login();
        }

        public static void ResetSource()
        {
            lstPHvsHs = new List<LienKetPHvsHS>();
            lstStudents = new List<ThongTinHS>();
            StudentSeclect = new ThongTinHS();
            lstLops = new List<Lop>();
            lstKhois = new List<Khoi>();
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
