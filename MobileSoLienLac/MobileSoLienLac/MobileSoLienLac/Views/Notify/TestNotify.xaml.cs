using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;
using Plugin.Toasts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Notify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNotify : ContentPage
    {
        public string StrConn = @"SERVER=125.212.218.20;Database = nxtckedu_HeThongSoLienLac; uid= nxtckedu_sa; pwd= H*P*T-1999";
        public SqlConnection conn = null;

        public delegate void GetNotify();
        public event GetNotify OnGetNotify;

        List<DiemDanh> lst = new List<DiemDanh>();

        public TestNotify()
        {
            InitializeComponent();

            lst = LoadList();
            this.OnGetNotify += new GetNotify(This_OnGetNotify);


            //try
            //{
            //    SqlDependency.Stop(StrConn);
            //    SqlDependency.Start(
            //        @"SERVER=125.212.218.20;Database = nxtckedu_HeThongSoLienLac; uid= nxtckedu_sa; pwd= H*P*T-1999");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
            conn = new SqlConnection(StrConn);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            ToastMessage("Hello","Chào bạn");
        }

        public void This_OnGetNotify()
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;
            if (i.InvokeRequired)//tab
            {
                GetNotify dd = new GetNotify(This_OnGetNotify);
                i.BeginInvoke(dd, null);
                return;
            }
            ShowNotify();
        }

        public List<DiemDanh> LoadList()
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("SELECT * FROM nxtckedu_sa.DiemDanh WHERE IDHocSinh = 1", conn);
            cmd.Notification = null;

            SqlDependency de = new SqlDependency(cmd);
            de.OnChange += new OnChangeEventHandler(OnChage);

            dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
            List<DiemDanh> val = new List<DiemDanh>();
            foreach (DataRow dr in dt.Rows)
            {
                val.Add(new DiemDanh(dr));
            }

            return val;
        }

        public void ShowNotify()
        {
            List<DiemDanh> val = LoadList();
            IEnumerable<DiemDanh> differenceQuery = null;

            if (val.Count > lst.Count)
            {
                differenceQuery = val.Where(x => !lst.Contains(x));
            }

            foreach (var dd in differenceQuery)
            {
                ToastMessage("Vắng học", dd.NgayNghi.ToString());
            }
        }

        private void OnChage(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency de = sender as SqlDependency;
            de.OnChange -= OnChage;
            if (OnGetNotify != null)
            {
                OnGetNotify();
            }
        }

        public void ToastMessage(String title, String description)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var notifier = DependencyService.Get<IToastNotificator>();
                var options = new NotificationOptions()
                {
                    Title = title,
                    Description = description
                };
                var result = await notifier.Notify(options);
            });
        }
    }
}