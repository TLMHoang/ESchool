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
        public static List<Khoi> lstKhois;
        public static List<LoaiDiem> lstLoaiDiems = new List<LoaiDiem>();
        public static List<LoaiHanhKiem> lstLoaiHanhKiems = new List<LoaiHanhKiem>();
        public static List<LoaiHocSinh> lstLoaiHocSinhs = new List<LoaiHocSinh>();
        public static List<LoaiThongBao> lstLoaiThongBaos = new List<LoaiThongBao>();

        #region Notify

        public static List<NotifyModel> LstThongBaoTruongs = new List<NotifyModel>();
        public static List<NotifyModel> LstThongBaoLops = new List<NotifyModel>();
        public static List<NotifyModel> LstThongBaoHSs = new List<NotifyModel>();

        #endregion
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
            lstLoaiDiems = new List<LoaiDiem>();
            lstLoaiHanhKiems = new List<LoaiHanhKiem>();
            lstLoaiHocSinhs = new List<LoaiHocSinh>();
            lstLoaiThongBaos = new List<LoaiThongBao>();

            LstThongBaoTruongs = new List<NotifyModel>();
            LstThongBaoLops = new List<NotifyModel>();
            LstThongBaoHSs = new List<NotifyModel>();

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
