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

        public async Task<ValueDTO<NotifyModel>> GetDataSchool()
        {
            ValueDTO<NotifyModel> val = new ValueDTO<NotifyModel>();
            DataTableSQL dtSql = await ExecuteQuery("SelectListThongBaoTruong");

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new NotifyModel(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

        public async Task<ValueDTO<NotifyModel>> GetDataClass(int IDClass)
        {
            ValueDTO<NotifyModel> val = new ValueDTO<NotifyModel>();
            DataTableSQL dtSql = await ExecuteQuery("SelectListThongBaoLop",
                new SqlParameter("@IDLop", SqlDbType.Int) { Value = IDClass });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new NotifyModel(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

        public async Task<ValueDTO<NotifyModel>> GetDataStudent(int IDHocSinh)
        {
            ValueDTO<NotifyModel> val = new ValueDTO<NotifyModel>();
            DataTableSQL dtSql = await ExecuteQuery("SelectListThongBaoHS",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) { Value = IDHocSinh });
            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new NotifyModel(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

    }
}
