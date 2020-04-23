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
        public int HeSo { get; set; }

        public LoaiDiem() { ID = -1; TenLoaiDiem = ""; HeSo = 0; }

        public LoaiDiem(int iD, string tenLoaiDiem, int heSo)
        {
            ID = iD;
            TenLoaiDiem = tenLoaiDiem;
            HeSo = heSo;
        }

        public LoaiDiem(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            TenLoaiDiem = dr["TenLoaiDiem"].ToString();
            HeSo = Convert.IsDBNull(dr["HeSo"]) ? -1 : Convert.ToInt32(dr["HeSo"]);
        }

        #region Handle with Database

        public async Task<ValueDTO<LoaiDiem>> GetData()
        {
            ValueDTO<LoaiDiem> val = new ValueDTO<LoaiDiem>();
            DataTableSQL dtSql = await ExecuteQuery("SelectLoaiDiem",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new LoaiDiem(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

        #endregion
    }
}
