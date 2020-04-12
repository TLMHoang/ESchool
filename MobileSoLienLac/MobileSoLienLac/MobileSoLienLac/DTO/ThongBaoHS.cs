using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class ThongBaoHS : Helper
    {
        public int ID { get; set; }
        public int IDHocSinh { get; set; }
        public int IDLoaiThongBao { get; set; }
        public string NoiDung { get; set; }
        public DateTime Ngay { get; set; }

        public ThongBaoHS()
        {
            ID = -1;
            IDHocSinh = -1;
            IDLoaiThongBao = -1;
            NoiDung = "";
            Ngay = DateTime.Now;
        }

        public ThongBaoHS(int iD, int idHocSinh, int iDLoaiThongBao, string noiDung, DateTime ngay)
        {
            ID = iD;
            IDHocSinh = idHocSinh;
            IDLoaiThongBao = iDLoaiThongBao;
            NoiDung = noiDung;
            Ngay = ngay;
        }

        public ThongBaoHS(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]); 
            IDHocSinh = Convert.IsDBNull(dr["IDHocSinh"]) ? -1 : Convert.ToInt32(dr["IDHocSinh"]);
            IDLoaiThongBao = Convert.IsDBNull(dr["IDLoaiThongBao"]) ? -1 : Convert.ToInt32(dr["IDLoaiThongBao"]); ;
            NoiDung = dr["NoiDung"].ToString();
            Ngay = Convert.ToDateTime(dr["Ngay"]);
        }

        public async Task<ValueDTO<ThongBaoHS>> GetData()
        {
            ValueDTO<ThongBaoHS> val = new ValueDTO<ThongBaoHS>();
            DataTableSQL dtSql = await ExecuteQuery("SelectThongBaoHS",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });

            if (dtSql.Error != 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThongBaoHS(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

        public async Task<ValueDTO<ThongBaoHS>> GetContent(int ID)
        {
            ValueDTO<ThongBaoHS> val = new ValueDTO<ThongBaoHS>();
            DataTableSQL dtSql = await ExecuteQuery("SelectNoiDungThongBaoHS",
                new SqlParameter("@ID", SqlDbType.Int) {Value = ID});
            
            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThongBaoHS(dr));
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
