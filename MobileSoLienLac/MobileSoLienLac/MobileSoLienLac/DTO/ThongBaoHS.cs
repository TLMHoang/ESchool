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

        public async Task<DataTable> GetData()
        {
            return await ExecuteQuery("SelectThongBaoHS",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });
        }

        public List<ThongBaoHS> GetData(DataTable dt)
        {
            List<ThongBaoHS> lst = new List<ThongBaoHS>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new ThongBaoHS(dr));
            }

            return lst;
        }

        public ThongBaoHS GetData(DataRow dr)
        {
            return new ThongBaoHS(dr);
        }

        public async Task<DataTable> GetContent(int ID)
        {
            return await ExecuteQuery("SelectNoiDungThongBaoHS",
                new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
        }
    }
}
