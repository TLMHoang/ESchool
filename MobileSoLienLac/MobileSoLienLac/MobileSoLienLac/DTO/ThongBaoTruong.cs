using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class ThongBaoTruong : Helper
    {
        public int ID { get; set; }
        public int IDLoaiThongBao { get; set; }
        public string NoiDung { get; set; }
        public DateTime Ngay { get; set; }

        public ThongBaoTruong()
        {
            ID = -1;
            IDLoaiThongBao = -1;
            NoiDung = "";
            Ngay = DateTime.Now;
        }

        public ThongBaoTruong(int iD, int iDLoaiThongBao, string noiDung, DateTime ngay)
        {
            ID = iD;
            IDLoaiThongBao = iDLoaiThongBao;
            NoiDung = noiDung;
            Ngay = ngay;
        }

        public ThongBaoTruong(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]); ;
            IDLoaiThongBao = Convert.IsDBNull(dr["IDLoaiThongBao"]) ? -1 : Convert.ToInt32(dr["IDLoaiThongBao"]); ;
            NoiDung = dr["NoiDung"].ToString();
            Ngay = Convert.ToDateTime(dr["Ngay"]);
        }

        public async Task<ValueDTO<ThongBaoTruong>> GetData()
        {
            ValueDTO<ThongBaoTruong> val = new ValueDTO<ThongBaoTruong>();
            DataTableSQL dtSql = await ExecuteQuery("SelectThongBaoTruong",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });

            if (dtSql.Error != 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThongBaoTruong(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

        public async Task<ValueDTO<ThongBaoTruong>> GetContent(int ID)
        {
            ValueDTO<ThongBaoTruong> val = new ValueDTO<ThongBaoTruong>();
            DataTableSQL dtSql = await ExecuteQuery("SelectNoiDungThongBaoTruong",
                new SqlParameter("@ID", SqlDbType.Int) { Value = ID });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThongBaoTruong(dr));
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