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

        public async Task<ValueDTO<LoaiHanhKiem>> GetData()
        {
            ValueDTO<LoaiHanhKiem> val = new ValueDTO<LoaiHanhKiem>();
            DataTableSQL dtSql = await ExecuteQuery("SelectHanhKiem",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new LoaiHanhKiem(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
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
