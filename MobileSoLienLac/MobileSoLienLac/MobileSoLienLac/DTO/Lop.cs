using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class Lop : Helper
    {
        public int ID { get; set; }
        public int IDKhoi { get; set; }
        public string TenLop { get; set; }

        public Lop() 
        {
            ID = -1;
            IDKhoi = -1;
            TenLop = "";
        }

        public Lop(int iD, int iDKhoi, string tenLop)
        {
            ID = iD;
            IDKhoi = iDKhoi;
            TenLop = tenLop;
            
        }

        public Lop(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            IDKhoi = Convert.IsDBNull(dr["IDKhoi"]) ? -1 : Convert.ToInt32(dr["IDKhoi"]);
            TenLop = dr["TenLop"].ToString();
        }

        #region Handle with Database

        public async Task<DataTable> GetData()
        {
            return await ExecuteQuery("SelectLop",
                new SqlParameter("@ID", SqlDbType.Int) {Value = -1});
        }

        public List<Lop> GetData(DataTable dt)
        {
            List<Lop> lst = new List<Lop>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new Lop(dr));
            }

            return lst;
        }


        #endregion
    }
}
