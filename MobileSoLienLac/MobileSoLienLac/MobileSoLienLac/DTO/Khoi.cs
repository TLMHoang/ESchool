

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class Khoi
    {
        public int ID { get; set; }
        public string TenKhoi { get; set; }

        public Khoi() { ID = -1; TenKhoi = ""; }

        public Khoi(int iD, string tenKhoi)
        {
            ID = iD;
            TenKhoi = tenKhoi;

        }

        public Khoi(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            TenKhoi = dr["TenKhoi"].ToString();
        }

        #region Handle with Database

        public async Task<List<Khoi>> GetList()
        {
            List<Khoi> lst = new List<Khoi>();
            DataTable dt = await new Helper().ExecuteQuery("SelectKhoi",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new Khoi(dr));
            }

            return lst;
        }

        #endregion
    }
}
