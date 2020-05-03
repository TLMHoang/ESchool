using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Views.Notify;

namespace TestDependecy
{
    [TestClass]
    public class UnitTest1
    {
        public string StrConn = @"SERVER=125.212.218.20;Database = nxtckedu_HeThongSoLienLac; uid= nxtckedu_sa; pwd= H*P*T-1999";
        public SqlConnection conn = null;

        public delegate void GetNotify();
        public event TestNotify.GetNotify OnGetNotify;

        List<DiemDanh> lst = new List<DiemDanh>();

        [TestMethod]
        public void TestMethod1()
        {
            SqlDependency.Start(
                @"SERVER=125.212.218.20;Database = nxtckedu_HeThongSoLienLac; uid= nxtckedu_sa; pwd= H*P*T-1999");
        }

        public void This_OnGetNotify()
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;
            if (i.InvokeRequired)//tab
            {
                TestNotify.GetNotify dd = new TestNotify.GetNotify(This_OnGetNotify);
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
                //ToastMessage("Vắng học", dd.NgayNghi.ToString());
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
    }
}
