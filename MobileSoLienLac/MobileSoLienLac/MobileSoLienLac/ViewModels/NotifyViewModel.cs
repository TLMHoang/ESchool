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
        public int IDLoaiThongBao { get; set; }
        public DateTime Ngay { get; set; }


        public NotifyViewModel() { }
        public NotifyViewModel(int iD, int idLoaiThongBao, DateTime ngay)
        {
            ID = iD;
            IDLoaiThongBao = idLoaiThongBao;
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
                App.LstThongBaoTruongs = new NotifyModel().GetDataSchool(dt);
                return "";
            }
        }

        public async Task<string> NotifyClass()
        {
            DataTable dt = await new NotifyModel().GetDataClass();
            if (dt.Columns.Count == 1)
            {
                return new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"]));
            }
            else
            {
                App.LstThongBaoTruongs = new NotifyModel().GetDataClass(dt);
                return "";
            }
        }

        public async Task<string> NotifyStudent()
        {
            DataTable dt = await new NotifyModel().GetDataStudent();
            if (dt.Columns.Count == 1)
            {
                return new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"]));
            }
            else
            {
                App.LstThongBaoTruongs = new NotifyModel().GetDataStudent(dt);
                return "";
            }
        }
    }
}
