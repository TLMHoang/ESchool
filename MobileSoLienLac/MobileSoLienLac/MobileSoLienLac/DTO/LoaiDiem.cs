using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class LoaiDiem : Helper
    {
        public int ID { get; set; }
        public string TenLoaiDiem { get; set; }

        public LoaiDiem() { ID = -1; TenLoaiDiem = ""; }

        public LoaiDiem(int iD, string tenLoaiDiem)
        {
            ID = iD;
            TenLoaiDiem = tenLoaiDiem;

        }

        public LoaiDiem(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            TenLoaiDiem = dr["TenLoaiDiem"].ToString();
        }

        #region Handle with Database

        public async Task<DataTable> GetData()
        {

            return await ExecuteQuery("SelectLoaiDiem",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });
        }

        public List<LoaiDiem> GetData(DataTable dt)
        {
            List<LoaiDiem> lst = new List<LoaiDiem>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new LoaiDiem(dr));
            }

            return lst;
        }

        #endregion
    }
}
