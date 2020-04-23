using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class ThoiKhoaBieu : Helper
    {
        public int Thu { get; set; }
        public int Tiet { get; set; }
        public string TenMon { get; set; }

        public ThoiKhoaBieu() { }

        public ThoiKhoaBieu(DataRow dr)
        {
            Thu = Convert.IsDBNull(dr["Thu"]) ? -1 : Convert.ToInt32(dr["Thu"]);
            Tiet = Convert.IsDBNull(dr["Tiet"]) ? -1 : Convert.ToInt32(dr["Tiet"]);
            TenMon = dr["TenMon"].ToString();
        }

        public async Task<ValueDTO<ThoiKhoaBieu>> GetData(int IDLop)
        {
            DataTableSQL dtSql = await ExecuteQuery("m_SelectThoiKhoaBieu",
                new SqlParameter("@IDLop", SqlDbType.Int) {Value = IDLop});
            ValueDTO<ThoiKhoaBieu> val = new ValueDTO<ThoiKhoaBieu>();

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThoiKhoaBieu(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

    }

    public class SortThoiKhoaBieu : IComparer<ThoiKhoaBieu>
    {
        public int Compare(ThoiKhoaBieu x, ThoiKhoaBieu y)
        {
            return x.Tiet.CompareTo(y.Tiet);
        }
    }
}
