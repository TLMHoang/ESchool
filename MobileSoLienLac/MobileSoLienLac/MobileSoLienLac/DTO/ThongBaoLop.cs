using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    class ThongBaoLop : Helper
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

        public async Task<DataTable> GetData()
        {
            return await ExecuteQuery("SelectThongBaoLop",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });
        }

        public List<ThongBaoLop> GetData(DataTable dt)
        {
            List<ThongBaoLop> lst = new List<ThongBaoLop>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new ThongBaoLop(dr));
            }

            return lst;
        }

        public ThongBaoLop GetData(DataRow dr)
        {
            return new ThongBaoLop(dr);
        }

        public async Task<DataTable> GetContent(int ID)
        {
            return await ExecuteQuery("SelectNoiDungThongBaoLop",
                new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
        }
    }
}
