using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.Models
{
    public class NotifyModel : Helper
    {
        public int ID { get; set; }
        public int IDLoaiThongBao { get; set; }
        public DateTime Ngay { get; set; }

        public NotifyModel()
        {
            ID = -1;
            IDLoaiThongBao = -1;
            Ngay = DateTime.Now;
        }

        public NotifyModel(int iD, int iDLoaiThongBao, DateTime ngay)
        {
            ID = iD;
            IDLoaiThongBao = iDLoaiThongBao;
            Ngay = ngay;
        }

        public NotifyModel(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]); ;
            IDLoaiThongBao = Convert.IsDBNull(dr["IDLoaiThongBao"]) ? -1 : Convert.ToInt32(dr["IDLoaiThongBao"]); ;
            Ngay = Convert.ToDateTime(dr["Ngay"]);
        }

        public async Task<DataTable> GetDataSchool()
        {
            return await ExecuteQuery("SelectListThongBaoTruong");
        }

        public List<NotifyModel> GetDataSchool(DataTable dt)
        {
            List<NotifyModel> lst = new List<NotifyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new NotifyModel(dr));
            }

            return lst;
        }

        public async Task<DataTable> GetDataClass()
        {
            return await ExecuteQuery("SelectListThongBaoLop");
        }

        public List<NotifyModel> GetDataClass(DataTable dt)
        {
            List<NotifyModel> lst = new List<NotifyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new NotifyModel(dr));
            }

            return lst;
        }

        public async Task<DataTable> GetDataStudent()
        {
            return await ExecuteQuery("SelectListThongBaoHS");
        }

        public List<NotifyModel> GetDataStudent(DataTable dt)
        {
            List<NotifyModel> lst = new List<NotifyModel>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new NotifyModel(dr));
            }

            return lst;
        }

    }
}
