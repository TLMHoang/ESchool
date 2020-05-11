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
        #endregion
        public App()
        {
            InitializeComponent();


            DependencyService.Register<MockDataStore>();
            MainPage = new TestNotify();
            //MainPage = new TestNotify();
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

        //public void Main_OnLoadNoyify()
        //{
        //    ISynchronizeInvoke i = (ISynchronizeInvoke)this;
        //    if (i.InvokeRequired)//tab
        //    {
        //        LoadNotify dd = new LoadNotify(Main_OnLoadNoyify);
        //        i.BeginInvoke(dd, null);
        //        return;
        //    }
        //}

        protected override void OnStart()
        {
            //SqlDependency.Stop(new Helper().connStr);
            //SqlDependency.Start(new Helper().connStr);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
