using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class LoaiHanhKiem : Helper
    {
        public int ID { get; set; }
        public string TenHanhKiem { get; set; }

        public LoaiHanhKiem() { ID = -1; TenHanhKiem = ""; }

        public LoaiHanhKiem(int iD, string TenHanhKiem)
        {
            ID = iD;
            TenHanhKiem = TenHanhKiem;

        }

        public LoaiHanhKiem(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            TenHanhKiem = dr["TenHanhKiem"].ToString();
        }

        #region Handle with Database

        public async Task<DataTable> GetData()
        {

            return await ExecuteQuery("SelectHanhKiem",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });
        }

        public List<LoaiHanhKiem> GetData(DataTable dt)
        {
            List<LoaiHanhKiem> lst = new List<LoaiHanhKiem>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new LoaiHanhKiem(dr));
            }

            return lst;
        }

        #endregion
    }
}
