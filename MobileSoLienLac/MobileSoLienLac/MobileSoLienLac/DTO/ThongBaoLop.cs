using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class ThongBaoLop : Helper
    {
        public int ID { get; set; }
        public int IDLop { get; set; }
        public int IDLoaiThongBao { get; set; }
        public string NoiDung { get; set; }
        public DateTime Ngay { get; set; }

        public ThongBaoLop()
        {
            ID = -1;
            IDLop = -1;
            IDLoaiThongBao = -1;
            NoiDung = "";
            Ngay = DateTime.Now;
        }

        public ThongBaoLop(int iD, int idLop, int iDLoaiThongBao, string noiDung, DateTime ngay)
        {
            ID = iD;
            IDLop = idLop;
            IDLoaiThongBao = iDLoaiThongBao;
            NoiDung = noiDung;
            Ngay = ngay;
        }

        public ThongBaoLop(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            IDLop = Convert.IsDBNull(dr["IDLop"]) ? -1 : Convert.ToInt32(dr["IDLop"]);
            IDLoaiThongBao = Convert.IsDBNull(dr["IDLoaiThongBao"]) ? -1 : Convert.ToInt32(dr["IDLoaiThongBao"]); ;
            NoiDung = dr["NoiDung"].ToString();
            Ngay = Convert.ToDateTime(dr["Ngay"]);
        }

        public async Task<ValueDTO<ThongBaoLop>> GetData()
        {
            ValueDTO<ThongBaoLop> val = new ValueDTO<ThongBaoLop>();
            DataTableSQL dtSql = await ExecuteQuery("SelectThongBaoLop",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });

            if (dtSql.Error != 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThongBaoLop(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

        public async Task<ValueDTO<ThongBaoLop>> GetContent(int ID)
        {
            ValueDTO<ThongBaoLop> val = new ValueDTO<ThongBaoLop>();
            DataTableSQL dtSql = await ExecuteQuery("SelectNoiDungThongBaoLop",
                new SqlParameter("@ID", SqlDbType.Int) { Value = ID });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThongBaoLop(dr));
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
