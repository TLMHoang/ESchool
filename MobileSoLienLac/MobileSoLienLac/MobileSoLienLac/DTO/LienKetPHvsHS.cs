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

        public async Task<ValueDTO<LienKetPHvsHS>> GetData(int IDTaiKhoan)
        {
            ValueDTO<LienKetPHvsHS> val = new ValueDTO<LienKetPHvsHS>();
            DataTableSQL dtSql = await ExecuteQuery("SelectLayHSQuanLy",
                new SqlParameter("@IDTaiKhoan", SqlDbType.Int) {Value = IDTaiKhoan});

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new LienKetPHvsHS(dr));
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
