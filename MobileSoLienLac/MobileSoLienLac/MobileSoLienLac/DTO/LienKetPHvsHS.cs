using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class LienKetPHvsHS :Helper
    {
        public  int IDHocSinh { get; set; }
        public int IDTaiKhoan { get; set; }

        public LienKetPHvsHS()
        {
            IDHocSinh = -1;
            IDTaiKhoan = -1;
        }

        public LienKetPHvsHS(int iDHocSinh, int iDTaiKhoan)
        {
            IDHocSinh = iDHocSinh;
            IDTaiKhoan = iDTaiKhoan;
        }

        public LienKetPHvsHS(DataRow dr)
        {
            IDHocSinh = Convert.IsDBNull(dr["IDHocSinh"]) ? -1 : Convert.ToInt32(dr["IDHocSinh"]);
            IDTaiKhoan = Convert.IsDBNull(dr["IDTaiKhoan"]) ? -1 : Convert.ToInt32(dr["IDTaiKhoan"]);
        }

        public async Task<DataTable> GetData(int IDTaiKhoan)
        {

            return await ExecuteQuery("SelectLayHSQuanLy",
                new SqlParameter("@IDTaiKhoan", SqlDbType.Int) {Value = IDTaiKhoan});
        }

        public List<LienKetPHvsHS> GetData(DataTable dt)
        {
            List<LienKetPHvsHS> lst = new List<LienKetPHvsHS>();

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new LienKetPHvsHS(dr));
            }

            return lst;
        }

    }
}
