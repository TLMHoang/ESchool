using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.ViewModels
{
    public class NotifyViewModel
    {
        public int ID { get; set; }
        public string TenThongBao { get; set; }
        public DateTime Ngay { get; set; }


        public NotifyViewModel() { }
        public NotifyViewModel(int iD, string tenThongBao, DateTime ngay)
        {
            ID = iD;
            TenThongBao = tenThongBao;
            Ngay = ngay;
        }

        public async Task<string> NotifySchool()
        {
            DataTable dt = await new NotifyModel().GetDataSchool();
            if (dt.Columns.Count == 1)
            {
                return new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"]));
            }
            else
            {
                App.LstThongBaoTruongs = new List<NotifyModel>();
                App.LstThongBaoTruongs = new NotifyModel().GetDataSchool(dt);
                return "";
            }
        }

        public async Task<string> NotifyClass()
        {
            DataTable dt = await new NotifyModel().GetDataClass(App.StudentSeclect.IDLop);
            if (dt.Columns.Count == 1)
            {
                return new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"]));
            }
            else
            {
                App.LstThongBaoLops = new List<NotifyModel>();
                App.LstThongBaoLops = new NotifyModel().GetDataClass(dt);
                return "";
            }
        }

        public async Task<string> NotifyStudent()
        {
            DataTable dt = await new NotifyModel().GetDataStudent(App.StudentSeclect.ID);
            if (dt.Columns.Count == 1)
            {
                return new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"]));
            }
            else
            {
                App.LstThongBaoHSs = new List<NotifyModel>();
                App.LstThongBaoHSs = new NotifyModel().GetDataStudent(dt);
                return "";
            }
        }
    }
}
