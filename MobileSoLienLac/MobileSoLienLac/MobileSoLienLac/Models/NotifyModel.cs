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
        public string TenThongBao { get; set; }
        public DateTime Ngay { get; set; }

        public NotifyModel()
        {
            ID = -1;
            TenThongBao = "";
            Ngay = DateTime.Now;
        }

        public NotifyModel(int iD, string tenThongBao, DateTime ngay)
        {
            ID = iD;
            TenThongBao = tenThongBao;
            Ngay = ngay;
        }

        public NotifyModel(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]); ;
            TenThongBao = dr["TenThongBao"].ToString(); ;
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

        public async Task<DataTable> GetDataClass(int IDClass)
        {
            return await ExecuteQuery("SelectListThongBaoLop", 
                new SqlParameter("@IDLop", SqlDbType.Int){Value = IDClass});
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

        public async Task<DataTable> GetDataStudent(int IDHocSinh)
        {
            return await ExecuteQuery("SelectListThongBaoHS",
            new SqlParameter("@IDHocSinh", SqlDbType.Int) { Value = IDHocSinh });
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
